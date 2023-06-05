namespace DestructionMapModel.Models;

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string  Address { get; set; }
    public int Age { get; set; }

    public User(string name, string surname, string address, int age)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Surname = surname;
        Address = address;
        Age = age;

    }
    
    public User(){}
    
}