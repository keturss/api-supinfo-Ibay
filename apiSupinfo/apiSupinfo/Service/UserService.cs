using apiSupinfo.Models.Service.Interface;
using ProjetWebAPI.DAL;
using ProjetWebAPI.Models.DTO;
using ProjetWebAPI.Models.Inputs;

namespace apiSupinfo.Models.Service;

public class UserService : IUserService
{
    private DbFactoryContext _context;
    private CartService _cartservice;

        public UserService(DbFactoryContext context)
        {
            _context = context;
        }
         
        public List<User> GetUsersList()
        {
            List<User> UsersList;
            try
            {
                UsersList = _context.Set<User>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return UsersList;
        }


        public User GetUserById(int Id)
        {
            User user;
            try
            {
                user = _context.Find<User>(Id);
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public User SaveUser(User user)
        {
            User TempUser=GetUserById(user.Id);
            if (TempUser != null)
            {
                TempUser.Username = user.Username;
                
                _context.Update<User>(TempUser);
            }   
           
            _context.SaveChanges();
            return TempUser;
        }
        

        public User CreateUser(User user)
        {
            if (UserExists(user.Username)) return user; //TODO message error
            
            _context.Add<User>(user);

            _context.SaveChanges();
            return user;
        }



        public User DeleteUser(int Id)
        {
            User user=GetUserById(Id);
            
            if (user == null) return user;
            
            _context.Remove<User>(user);
            _context.SaveChanges();
            
            return user;
        }

        public User Authenticate(User user)
        {
            return _context.Users.FirstOrDefault(x =>
                x.Username.ToLower() == user.Username.ToLower() && 
                x.Password == user.Password);
        }
        
        private bool UserExists(string name)
        {
            return _context.Users.Any(e => e.Username == name);
        }
}