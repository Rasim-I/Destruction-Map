using DestructionMapModel.Abstraction.IServices;
using DestructionMapModel.Implementation.Services.Utility;
using DestructionMapModel.Models;
using DestructionMapModel.Models.Enums;

namespace DestructionMapModel.Implementation.Services.QueryProcessing;

class WeaponSystemHandler : QueryHandler
{
    public override List<Event> HandleRequest(EventParameters eventParameters, IEventService eventService,
        List<Event> events)
    {
        try
        {
            List<Event> byWeaponSystem = events;

            if (eventParameters.WeaponSystem.Count != 0)
            {
                List<Event> sumOfSelected = new List<Event>();
                foreach (var weapon in eventParameters.WeaponSystem)
                {
                    //WeaponSystem weaponSystemToSearch = (WeaponSystem)Enum.Parse(typeof(WeaponSystem), eventParameters.WeaponSystem, true);

                    //List<Event> byWeaponSystem = eventService.GetByWeaponSystem(weapon).Intersect(events, new EventComparer()).ToList();
                    sumOfSelected.AddRange(eventService.GetByWeaponSystem(weapon));
                
                }

                byWeaponSystem = byWeaponSystem.Intersect(sumOfSelected, new EventComparer()).ToList();
            }
            
            //List<Event> byWeaponSystem = eventService.GetByWeaponSystem(weaponSystemToSearch).Intersect(events, new EventComparer()).ToList();

            if (Successor != null)
                return Successor.HandleRequest(eventParameters, eventService, byWeaponSystem);
            else
                return byWeaponSystem;
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