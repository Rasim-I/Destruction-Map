using DestructionMapModel.Abstraction.IServices;
using DestructionMapModel.Implementation.Services.Utility;
using DestructionMapModel.Models;
using Microsoft.IdentityModel.Tokens;

namespace DestructionMapModel.Implementation.Services.QueryProcessing;

class LocationHandler : QueryHandler
{

    public override List<Event> HandleRequest(EventParameters eventParameters, IEventService eventService,
        List<Event> events)
    {
        if (!eventParameters.Location.IsNullOrEmpty())
        {
            List<Event> eventsByLocation = eventService.GetByLocation(eventParameters.Location).Intersect(events, new EventComparer()).ToList();

            if (Successor != null)
                return Successor.HandleRequest(eventParameters, eventService, eventsByLocation);
            else
                return eventsByLocation;
        }
        else
        {
            if (Successor != null)
                return Successor.HandleRequest(eventParameters, eventService, events);
            else
                return events;
        }
        
    }

}