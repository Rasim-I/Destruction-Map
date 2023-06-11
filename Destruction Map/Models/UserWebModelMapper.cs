using DestructionMapModel.Models;

namespace Destruction_Map.Models;

public class UserWebModelMapper
{

    public UserWebModel ToWebModel(User user, string email)
    {
        return new UserWebModel()
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Address = user.Address,
            Email = email
        };
    }
    
}