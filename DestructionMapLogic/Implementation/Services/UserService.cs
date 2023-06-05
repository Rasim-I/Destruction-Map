using DestructionMapDAL;
using DestructionMapDAL.Entities;
using DestructionMapModel.Abstraction.IMappers;
using DestructionMapModel.Abstraction.IServices;
using DestructionMapModel.Models;
using Microsoft.IdentityModel.Tokens;

namespace DestructionMapModel.Implementation.Services;

public class UserService : IUserService 
{

    private IUnitOfWork _unitOfWork;
    private IMapper<UserEntity, User> _userMapper;

    public UserService(IUnitOfWork unitOfWork, IMapper<UserEntity, User> userMapper)
    {
        _unitOfWork = unitOfWork;
        _userMapper = userMapper;
    }


    public bool CreateUser(string id, string name, string surname, int age, string address)
    {
        if (_unitOfWork.Users.Find(u => u.Id == id).FirstOrDefault() == null)
        {
            _unitOfWork.Users.Create(new UserEntity()
                { Id = id, Address = address, Age = age, Name = name, Surname = surname });
            _unitOfWork.Save();
            return true;
        }
        return false;

    }

    public bool UpdateUser(string id, string name, string surname, int age, string address)
    {
        UserEntity? userToChange = _unitOfWork.Users.Find(u=> u.Id == id).FirstOrDefault();
        if (userToChange != null)
        {
            userToChange.Name = name.IsNullOrEmpty() ? userToChange.Name : userToChange.Name = name;
            userToChange.Surname = surname.IsNullOrEmpty() ? userToChange.Surname : userToChange.Surname = surname;
            userToChange.Age = age == 0 ? userToChange.Age : userToChange.Age = age;
            userToChange.Address = address.IsNullOrEmpty() ? userToChange.Address : userToChange.Address = address;

            _unitOfWork.Save();
            return true;
        }

        return false;
    }

    public User GetUserById(string Id)
    {
        UserEntity? userEntity = _unitOfWork.Users.Find(u => u.Id == Id).FirstOrDefault();
        return  _userMapper.ToModel(userEntity);
    }
    
}
