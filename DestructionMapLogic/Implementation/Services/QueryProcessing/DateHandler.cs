using DestructionMapModel.Abstraction.IServices;
using DestructionMapModel.Implementation.Services.Utility;
using DestructionMapModel.Models;

namespace DestructionMapModel.Implementation.Services.QueryProcessing;

class DateHandler : QueryHandler
{
    public override List<Event> HandleRequest(EventParameters eventParameters, IEventService eventService,
        List<Event> events)
    {
        if (eventParameters.EventDate != DateTime.MinValue)
        {
            List<Event> eventsByDate = eventService.GetByDate(eventParameters.EventDate).Intersect(events, new EventComparer()).ToList();

            if (Successor != null)
                return Successor.HandleRequest(eventParameters, eventService, eventsByDate);
            else
                return eventsByDate;
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