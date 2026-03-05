using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogapibackend.Models;
using blogapibackend.Models.DTO;
using blogapibackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace blogapibackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _data;

        public UserController(UserService dataFromService)
        {
            _data = dataFromService;
        }

        //Function to add our user that will be type of CreateAccountDTO called UserToAdd. This will return bool once our user is added.
        //Add User
        [HttpPost("AddUser")]
        public bool AddUser(CreateAccountDTO UserToAdd)
        {
            return _data.AddUser(UserToAdd);
        }

        //GetAllUsers
        [HttpGet("GetAllUsers")]
        public IEnumerable<UserModel>GetAllUsers()
        {
            return _data.GetAllUsers();
        }

        //GetUserByUserName
        [HttpGet("GetUserByUserName")]
        public UserIdDTO GetUserDTOUsername(string username)
        {
            return _data.GetUserIdDTOByUserName(username);
        }

        //LoginMethod (Login Endpoint)
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO User)
        {
            return _data.Login(User);
        }
    }
}