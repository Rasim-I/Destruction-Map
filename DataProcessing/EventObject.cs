namespace DataProcessing;

public class Source
{
    public string Id { get; set; }
    public string Path { get; set; }
    public string Description { get; set; }
}

public class Filter
{
    public string Key { get; set; }
    public string Value { get; set; }
}

public class EventObject
{
    public string Id { get; set; }
    
    public string Date { get; set; }
    
    public string Latitude { get; set; }
     
    public string Location { get; set; }
    
    public string Description { get; set; }
    
    public List<Source> Sources { get; set; }
    
    public List<Filter> Filters { get; set; }

}