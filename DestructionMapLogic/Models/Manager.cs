namespace DestructionMapModel.Models;

public class Manager
{
    
    public string Id { get; set; }
    public string User_Id { get; set; }

    public Manager(string userId)
    {
        Id = Guid.NewGuid().ToString();
        User_Id = userId;
    }
    
    public Manager(){}
    
}