using DestructionMapModel.Models;

namespace DestructionMapModel.Abstraction.IServices;

public interface IUserService
{
    public bool CreateUser(string id, string name, string surname, int age, string address);

    public bool UpdateUser(string id, string name, string surname, int age, string address);

    public User GetUserById(string id);

}