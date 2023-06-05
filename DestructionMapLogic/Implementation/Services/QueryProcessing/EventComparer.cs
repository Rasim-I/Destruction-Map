using DestructionMapModel.Models;

namespace DestructionMapModel.Implementation.Services.QueryProcessing;

public class EventComparer : IEqualityComparer<Event>
{
    public bool Equals(Event x, Event y)
    {
        //TODO - Add null handling
        return x.Id == y.Id;
    }

    public int GetHashCode(Event obj)
    {
        return obj.Id.GetHashCode();
    }
    
}