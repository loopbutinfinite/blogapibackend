using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogapibackend.Models.DTO;
using blogapibackend.Services.Context;
using Microsoft.EntityFrameworkCore;

namespace blogapibackend.Services
{
    public class UserService
    {
        private readonly DataContext _context;
        public UserService(DataContext dataContext)
        {
            _context = dataContext;
        }
        public bool AddUser(CreateAccountDTO userToAdd)
        {
            throw new NotFiniteNumberException();
        }
    }
}