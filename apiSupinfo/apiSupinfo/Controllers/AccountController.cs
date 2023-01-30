using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using apiSupinfo.Models.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjetWebAPI.DAL;
using ProjetWebAPI.Models.DTO;
using ProjetWebAPI.Models.Inputs;

namespace DualAuthCore.Controllers
{
    
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        
        
        public AccountController(IConfiguration config,IUserService service , IMapper mapper)
        {
            _config = config;
            _userService = service;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Authorize]
        public ActionResult<List<User>> GetAllUsers()
        {
            var listOfU =  _userService.GetUsersList();
            var userViewM = _mapper.Map<List<User>>(listOfU);
            if (listOfU == null)
                return NotFound(); 
            return Ok(userViewM);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userService.GetUserById(id);
            var userViewM = _mapper.Map<User>(user);
            if (user == null)
                return NotFound();
            return Ok(userViewM);
        }
        
        //Update user
        [HttpPut]
        [Authorize]
        public ActionResult<User> SaveUser([FromForm] UserUpdateInput input)
        {
            var userLogin = _mapper.Map<User>(input);
            var user = _userService.Authenticate(userLogin);
            var currentUser = GetCurrentUser();
            if (currentUser.Id != user.Id) return BadRequest(user.Id);
            var model=_userService.SaveUser(user);
            var userViewM = _mapper.Map<User>(model);
            return Ok(userViewM);
        }
        

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<User> CreateUser([FromForm] UserCreateInput input)
        {
            var user = _mapper.Map<User>(input);
            var model = _userService.CreateUser(user);
            return Ok(model);
        }
        
        
        [HttpDelete]
        [Authorize]
        public ActionResult<User> DeleteUser(int id)
        {
            var currentUser = GetCurrentUser();
            if (currentUser.Id != id) return BadRequest("Not same user");
            var model = _userService.DeleteUser(id);
            return Ok(model);
        }
        
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<string> Login([FromForm] UserLoginInput input)
        {
            var userLogin = _mapper.Map<User>(input);
            var user = _userService.Authenticate(userLogin);
            if (user == null) return NotFound("user not found");
            
            var token = GenerateToken(user);
            return Ok(token);

        }



        // To generate token
        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60*24*7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        private  User GetCurrentUser()
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity) return null;
            
            var userClaims = identity.Claims;
            return new User
            {
                Id = Int16.Parse(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value),
                Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
            };
        }
    }
    
   
}