namespace DestructionMapModel.Models;

public class Source
{
    public string Id { get; set; }
    public string Link { get; set; }
    public string Event_Id { get; set; }

    public Source(string link, string eventId)
    {
        Id = Guid.NewGuid().ToString();
        Link = link;
        Event_Id = eventId;
    }

    public Source(){}
}