using DestructionMapModel.Abstraction.IServices;
using DestructionMapModel.Implementation.Services.Utility;
using DestructionMapModel.Models;
using DestructionMapModel.Models.Enums;

namespace DestructionMapModel.Implementation.Services.QueryProcessing;

class BuildingTypeHandler : QueryHandler
{
    public override List<Event> HandleRequest(EventParameters eventParameters, IEventService eventService,
        List<Event> events)
    {
        try
        {
            List<Event> byBuildingType = events;

            if (eventParameters.BuildingType.Count != 0)
            {
                List<Event> sumOfSelected = new List<Event>();
            
                foreach (var building in eventParameters.BuildingType)
                {
                    //Console.WriteLine(building.ToString() + "--------------------------");
                    sumOfSelected.AddRange(eventService.GetByBuildingType(building));
                }

                byBuildingType = byBuildingType.Intersect(sumOfSelected, new EventComparer()).ToList();
            }
            

            //BuildingType buildingTypeToSearch = (BuildingType)Enum.Parse(typeof(BuildingType), eventParameters.BuildingType, true);
            //List<Event> byBuildingType = eventService.GetByBuildingType(buildingTypeToSearch).Intersect(events, new EventComparer()).ToList();

            if (Successor != null)
                return Successor.HandleRequest(eventParameters, eventService, byBuildingType);
            else
                return byBuildingType;
        }
        catch
        {
            if (Successor != null)
                return Successor.HandleRequest(eventParameters, eventService, events);
            else
                return events;
        }
        
    }
}