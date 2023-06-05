// See https://aka.ms/new-console-template for more information

using System.Globalization;
using System.Text.Json.Serialization;
using DataProcessing;
using Newtonsoft.Json;
using DestructionMapDAL;
using DestructionMapDAL.Entities;
using DestructionMapDAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Type = DestructionMapDAL.Entities.Enums.Type;

class Program
{
    public static void Main(String[] args)
    {

        string basePath = AppContext.BaseDirectory;
        string filePath = Path.Combine(basePath, "..", "..", "..", "ukr-civharm-2023-05-28.json");
        //string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ukr-civharm-2023-05-28.json");
        string jsonData = File.ReadAllText(filePath);

        List<EventObject> eventObjects = JsonConvert.DeserializeObject<List<EventObject>>(jsonData);

        foreach (var ev in eventObjects)
        {
            foreach (var ve in eventObjects)
            {
                if (ve.Id == ev.Id)
                {
                    Console.WriteLine("Same ID!  " + ve.Id );
                }
            }
        }
        
        Console.WriteLine(eventObjects.Count);

        /*
        foreach (EventObject evObj in eventObjects)
        {
            foreach (var filter in evObj.Filters)
            {
                Console.WriteLine(filter.Key + " ----- " + filter.Value);
            }
        }
        */
        
        EventEntityMapper eventEntityMapper = new EventEntityMapper();
        List<EventEntity> eventParsed = eventObjects.ConvertAll(e => eventEntityMapper.ToEventEntity(e));
        //EventEntity alreadySaved = eventParsed.Where(e => e.Id == "CIV0001").FirstOrDefault();
        //eventParsed.Remove(alreadySaved);

        //EventObject FirstEventObject = eventObjects.FirstOrDefault();
        //Console.WriteLine(FirstEventObject.Filters.Where(e => e.Key == "Type of area affected").FirstOrDefault().Value);
        //Console.WriteLine(FirstEventObject.Filters.Where(e => e.Key == "Weapon System").FirstOrDefault().Value);

        //Console.WriteLine("----------");
        
        //EventEntity FirstEventParsed = eventParsed.FirstOrDefault();
        //Console.WriteLine(FirstEventParsed.BuildingType.ToString());
        //Console.WriteLine(FirstEventParsed.WeaponSystem.ToString());
        
        
        //Console.WriteLine(eventParsed.Id + " -- " + eventParsed.EventDate + " -- " + eventParsed.Description + 
        //                  " -- " + eventParsed.Location + " -- " + eventParsed.WeaponSystem.ToString() + " -- " 
        //                  + eventParsed.BuildingType.ToString() + " -- " + eventParsed.SourceList.Count);

        //foreach (var source in eventParsed.SourceList)
        {
            //Console.WriteLine(source.Id + " -- " + source.Event_Id + " -- " + source.Link);
        }

        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=destructiondb;Trusted_Connection=True";
        var optionBuilder = new DbContextOptionsBuilder<DestructionMapContext>();
        optionBuilder.UseSqlServer(connectionString);
        
        
        using (var dbContext = new DestructionMapContext(optionBuilder.Options))
        {
            IUnitOfWork unitOfWork = new UnitOfWork(dbContext);


            List<EventEntity> eventsInDb = unitOfWork.Events.GetAll().ToList();
            foreach (var eventToSave in eventParsed)
            {
                
                
                //unitOfWork.Events.Create(eventToSave);
                
                
                foreach (var sourceForEvent in eventToSave.SourceList)
                {
                    //Console.WriteLine(sourceForEvent.Id + " - " + sourceForEvent.Event_Id);
                    //unitOfWork.Sources.Create(sourceForEvent);
                    
                    
                }
                //unitOfWork.Save();
            }
            
            
            
            
            //unitOfWork.Events.Create(eventParsed);
            //foreach (var source in eventParsed.SourceList)
            {
                //unitOfWork.Sources.Create(source);
            }

            foreach (var eventSaved in unitOfWork.Events.GetAll())
            {
                Console.WriteLine(eventSaved.Id);
            }
            
        }




    }
    

    
    public class EventEntityMapper
    {

        public EventEntity ToEventEntity(EventObject eventObject)
        {
            string dateString = eventObject.Date;
            string dateFormat = "MM/dd/yyyy";
            
            
            DateTime.TryParseExact(dateString, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime parsedData);
            
            
            
            BuildingType buildingType = default;
            WeaponSystem weaponSystem = default;
            
            foreach (var filter in eventObject.Filters)
            {
                if (filter.Key == "Type of area affected")
                { 
                Enum.TryParse(filter.Value.Replace(" ", "_"), out buildingType) ;
                }else if (filter.Key == "Weapon System")
                {
                    Enum.TryParse(filter.Value.Replace(" ", "_"), out weaponSystem);
                }
            }

            List<SourceEntity> sources = new List<SourceEntity>();
            foreach (var source in eventObject.Sources)
            {
                sources.Add(new SourceEntity()
                    { Id = Guid.NewGuid().ToString(), Event_Id = eventObject.Id, Link = source.Path });

            }
            
            return new EventEntity()
            {
                Id = eventObject.Id,
                Location = eventObject.Location,
                Description = eventObject.Description,
                EventDate = parsedData,
                BuildingType = buildingType,
                WeaponSystem = weaponSystem,
                User_Id = "",
                Type = Type.Event,
                SourceList = sources
            };

        }

        public static List<SourceEntity> GetSources(EventObject eventObject)
        {
            List<SourceEntity> sources = new List<SourceEntity>();
            foreach (var source in eventObject.Sources)
            {
                sources.Add(new SourceEntity()
                    { Id = Guid.NewGuid().ToString(), Event_Id = eventObject.Id, Link = source.Path });

            }
            return sources;
        }

    }
    
    
    
}



