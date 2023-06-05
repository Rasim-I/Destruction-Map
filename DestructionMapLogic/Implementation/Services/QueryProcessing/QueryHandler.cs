using DestructionMapModel.Abstraction.IServices;
using DestructionMapModel.Implementation.Services.Utility;
using DestructionMapModel.Models;

namespace DestructionMapModel.Implementation.Services.QueryProcessing;

abstract class QueryHandler
{
    public QueryHandler Successor { get; set; }

    public abstract List<Event> HandleRequest(EventParameters eventParameters, IEventService eventService, List<Event> events);

}