using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public bool AddUser(CreateAccountDTO UserToAdd)
        {
            return _data.AddUser(UserToAdd);
        }
    }
}