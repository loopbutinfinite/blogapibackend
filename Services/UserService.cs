using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using blogapibackend.Models;
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

        //We need a helper method to check if the user exists in our database, to avoid adding duplicate users to our database.
        public bool DoesUserExist(string username)
        {
            //Here we are going to check our database tables to see if the user exists.
            return _context.UserInfo.SingleOrDefault(user => user.Username == username) != null;
        }

        public bool AddUser(CreateAccountDTO userToAdd)
        {
            //We are going to need another helper function that will help us hash our password
            //We need to set our new newUser.Id to our UserToAdd.Id
            //Username
            //Salt
            //Hash

            //Then we'll add it to our DataContext
            //Save our changes
            //Return a bool to return true or false.
            
            bool result = false;

            if(userToAdd.Username != null && !DoesUserExist(userToAdd.Username))
            {
                UserModel newUser = new UserModel();

                var HashedPassword = HashPassword(userToAdd.Password);

                newUser.Id = userToAdd.Id;
                newUser.Username = userToAdd.Username;

                newUser.Salt = HashedPassword.Salt;
                newUser.Hash = HashedPassword.Hash;

                _context.Add(newUser);

                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        //Function that will help hash our password
        public PasswordDTO HashPassword(string? password)
        {
            PasswordDTO newHashedPassword = new PasswordDTO();

            byte[] SaltBytes = new byte[64];

            var provider = RandomNumberGenerator.Create();
            provider.GetNonZeroBytes(SaltBytes);

            var Salt = Convert.ToBase64String(SaltBytes);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password ?? "", SaltBytes, 10000, HashAlgorithmName.SHA256);

            var Hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            newHashedPassword.Salt = Salt;
            newHashedPassword.Hash = Hash;

            return newHashedPassword;
        }

        //Helper function to verify password
        public bool VerifyUserPassword(string? Password, string? StoredHash, string? StoredSalt)
        {
          //We need a check 
          if(StoredSalt == null)
            {
                return false;
            }  

            var SaltBytes = Convert.FromBase64String(StoredSalt);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Password ?? "", SaltBytes, 10000, HashAlgorithmName.SHA256);

            var newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return newHash == StoredHash;
        }
    }
}