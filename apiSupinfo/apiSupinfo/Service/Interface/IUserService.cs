using AutoMapper;
using ProjetWebAPI.Models.DTO;
using ProjetWebAPI.Models.Inputs;

namespace apiSupinfo.Models.Service.Interface;

public interface IUserService
{
    public List<User> GetUsersList();

    public User GetUserById(int Id);

    public User SaveUser(User user);

    public User CreateUser(User user);

    public User DeleteUser(int Id);

    public User Authenticate(User user);

}