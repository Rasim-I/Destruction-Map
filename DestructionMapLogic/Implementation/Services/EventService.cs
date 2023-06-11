using System.Text.RegularExpressions;
using DestructionMapDAL;
using DestructionMapDAL.Entities;
using DestructionMapModel.Abstraction.IMappers;
using DestructionMapModel.Abstraction.IServices;
using DestructionMapModel.Implementation.Services.QueryProcessing;
using DestructionMapModel.Implementation.Services.Utility;
using DestructionMapModel.Models;
using DestructionMapModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Type = DestructionMapModel.Models.Enums.Type;

namespace DestructionMapModel.Implementation.Services;

public class EventService : IEventService
{
    private IUnitOfWork _unitOfWork;
    private IMapper<EventEntity, Event> _eventMapper;
    private IMapper<SourceEntity, Source> _sourceMapper;

    public EventService(IUnitOfWork unitOfWork, IMapper<EventEntity, Event> eventMapper, IMapper<SourceEntity, Source> sourceMapper)
    {
        _unitOfWork = unitOfWork;
        _eventMapper = eventMapper;
        _sourceMapper = sourceMapper;
    }
    
    
    public List<Event> GetAll()
    {
        return new List<Event>(_unitOfWork.Events.GetAll_IncludeAll().ToList().ConvertAll(_eventMapper.ToModel));
    }

    public List<Event> GetByLocation(string region)
    {
        
        return new List<Event>(_unitOfWork.Events.GetByLocation(region.ToLower()).ToList().ConvertAll(_eventMapper.ToModel));
    }

    public List<Event> GetByLocationOnly(string location)
    {
        string pattern = $@"\b{Regex.Escape(location)}\b";
        
        var result = _unitOfWork.Events.GetByLocation(location.ToLower()).ToList();

        return result.Where(e => Regex.IsMatch(e.Location, pattern, RegexOptions.IgnoreCase))
            .Select(e => _eventMapper.ToModel(e)).ToList();

        //return new List<Event>(_unitOfWork.Events.GetByLocation(location.ToLower())
        //    .Where(e => Regex.IsMatch(e.Location, pattern, RegexOptions.IgnoreCase))
        //    .Select(e => _eventMapper.ToModel(e))).ToList();
    }

    public List<Event> GetByWeaponSystem(WeaponSystem weaponSystem)
    {
        return new List<Event>(_unitOfWork.Events
            .GetByWeaponSystem((DestructionMapDAL.Entities.Enums.WeaponSystem)weaponSystem).ToList()
            .ConvertAll(_eventMapper.ToModel));
    }

    public List<Event> GetByBuildingType(BuildingType buildingType)
    {
        return new List<Event>(_unitOfWork.Events
            .GetByBuildingType((DestructionMapDAL.Entities.Enums.BuildingType)buildingType).ToList()
            .ConvertAll(_eventMapper.ToModel));
    }

    public List<Event> GetByDate(DateTime eventDate)
    {
        return new List<Event>(_unitOfWork.Events
            .GetByDate(eventDate).ToList()
            .ConvertAll(_eventMapper.ToModel));
    }

    public List<Event> GetByDescription(string description)
    {
        string pattern = $@"\b{Regex.Escape(description)}\b";
        
        //return new List<Event>(_unitOfWork.Events
        //    .GetByDescription(description).ToList()
        //    .ConvertAll(_eventMapper.ToModel));

        var result = _unitOfWork.Events.GetByDescription(description.ToLower()).ToList();

        return result.Where(e => Regex.IsMatch(e.Description, pattern, RegexOptions.IgnoreCase))
            .Select(e => _eventMapper.ToModel(e)).ToList();
        
        return new List<Event>(_unitOfWork.Events.GetByDescription(description.ToLower())
            .Where(e => Regex.IsMatch(e.Description, pattern, RegexOptions.IgnoreCase))
            .Select(e => _eventMapper.ToModel(e))).ToList();
    }

    public List<Event> FindByParameters(EventParameters eventParameters)
    {
        QueryHandler byDate = new DateHandler();
        QueryHandler byWeaponSystem = new WeaponSystemHandler();
        QueryHandler byBuildingType = new BuildingTypeHandler();
        QueryHandler byLocation = new LocationHandler();

        byDate.Successor = byWeaponSystem;
        byWeaponSystem.Successor = byBuildingType;
        byBuildingType.Successor = byLocation;

        List<Event> result = byDate.HandleRequest(eventParameters, this, GetAll());

        return result;
    }

    public List<Event> FindByKeyWords(string keyWords)
    {
        string[] keyWordSplit = keyWords.Split();
        
        List<Event> match = new List<Event>();

        List<Event> matchByDescription = new List<Event>();
        List<Event> matchByLocation = new List<Event>();
        List<Event> matchByWeaponSystem = new List<Event>();
        List<Event> matchByBuildingType = new List<Event>();

        var e = GetByDescription("human");
        
        WeaponSystem weaponSystem;
        BuildingType buildingType;
        
        foreach (string word in keyWordSplit)
        {
            matchByDescription = GetByDescription(word);
            matchByLocation = GetByLocationOnly(word);

            if (Enum.TryParse(word, true, out weaponSystem))
                matchByWeaponSystem = GetByWeaponSystem(weaponSystem);

            if(Enum.TryParse(word, true, out buildingType))
                matchByBuildingType = GetByBuildingType(buildingType);
        }
        
        match.AddRange(matchByDescription);
        match.AddRange(matchByLocation);
        match.AddRange(matchByWeaponSystem);
        match.AddRange(matchByBuildingType);
        
        return match.Distinct(new EventComparer()).ToList();

    }

    public void CreateEvent(string userId, DateTime eventDate, string location, string description, BuildingType buildingType, WeaponSystem weaponSystem, string sources)
    {

        string[] sourcesSplit = sources.Split("\n");
        
        Event eventToSave = new Event(userId, eventDate, location, description, buildingType, weaponSystem);
        //eventToSave.Id = "TestEvent";  //REMOVE THIS
        eventToSave.Id = Guid.NewGuid().ToString();
        eventToSave.Type = Type.Proposal;

        List<Source> sourcesForEvent = new List<Source>();
        foreach (var source in sourcesSplit)
        {
            var sourceEntity = new Source(){Id = Guid.NewGuid().ToString(), Event_Id = eventToSave.Id, Link = source}; // Generate a unique Id for each Source entity
            sourcesForEvent.Add(sourceEntity);
        }
        eventToSave.SourceList = sourcesForEvent;

        
        _unitOfWork.Events.Create(_eventMapper.ToEntity(eventToSave));
        _unitOfWork.Save();
        
    }

    public List<Event> GetEventsToApprove(string userId)
    {

        List<Managers_Approvals> alreadyApprovedByThisManager = _unitOfWork.Approvals.Find(a => a.Manager_Id == userId).ToList();

        List<string> eventsToApprove = new List<string>();

        foreach (var eventId in alreadyApprovedByThisManager)
        {
            eventsToApprove.Add(eventId.Event_Id);
        }

        List<Event> eventProposals = _unitOfWork.Events.GetEventsToApprove().ToList().ConvertAll(_eventMapper.ToModel);
        List<Event> result = new List<Event>();
        result.AddRange(eventProposals);

        foreach (var eventId in eventsToApprove)
        {
            foreach (var resultEvent in eventProposals)
            {
                if (resultEvent.Id == eventId)
                    //result.Add(resultEvent);
                    result.Remove(resultEvent);
            }
            
        }
        
        return result;
    }


    public Dictionary<string, int> GetEventsIntensity()
    {
        Dictionary<string, int> eventIntensity = new Dictionary<string, int>();

        /*
        List<string> Locations = new List<string>()
        {
            "Crimea",
            "Mikolaiv",
            "Chernihiv",
            "Rivne",
            "Chernivtsi",
            "Ivano-Frankivsk",
            "Khmelnytskyi",
            "Lviv",
            "Ternopil",
            "Transcarpathia",
            "Volyn",
            "Cherkasy",
            "Kirovohrad",
            "Kyiv",
            "Odessa",
            "Vinnytsia",
            "Zhytomyr",
            "Sumy",
            "Dnipro",
            "Donetsk",
            "Kharkiv",
            "Luhansk",
            "Poltava",
            "Zaporizhzhia",
            "Kherson",
        };
        */
        
        Dictionary<string, string> Id_Location = new Dictionary<string, string>()
        {
            { "UKR283", "Crimea" },
            {"UKR284", "Mikolaiv"},
            {"UKR285", "Chernihiv"},
            {"UKR286", "Rivne"},
            {"UKR288", "Chernivtsi"},
            {"UKR289", "Ivano-Frankivsk"},
            {"UKR290", "Khmelnytskyi"},
            {"UKR291", "Lviv"},
            {"UKR292", "Ternopil'"},
            {"UKR293", "Transcarpathia"},
            {"UKR318", "Volyn"},
            {"UKR319", "Cherkasy"},
            {"UKR320", "Kirovohrad"},
            {"UKR321", "Kyiv"},
            {"UKR322", "Odessa"},
            {"UKR323", "Vinnytsia"},
            {"UKR324", "Zhytomyr"},
            {"UKR325", "Sumy"},
            {"UKR326", "Dnipro"},
            {"UKR327", "Donetsk"},
            {"UKR328", "Kharkiv"},
            {"UKR329", "Luhansk"},
            {"UKR330", "Poltava"},
            {"UKR331", "Zaporizhzhia"},
            {"UKR4826", "Kyiv"},
            {"UKR4827", "Kherson"},
            {"UKR5482", "Crimea"}
        };

        foreach (var location in Id_Location)
        {
            eventIntensity.Add(location.Key, _unitOfWork.Events.GetIntensityByLocation(location.Value)); 
        }

        return eventIntensity;
    }


}

