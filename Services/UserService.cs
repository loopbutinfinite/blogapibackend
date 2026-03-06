using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using blogapibackend.Models;
using blogapibackend.Models.DTO;
using blogapibackend.Services.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace blogapibackend.Services
{
    public class UserService : ControllerBase
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

            if (userToAdd.Username != null && !DoesUserExist(userToAdd.Username))
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
            if (StoredSalt == null)
            {
                return false;
            }

            var SaltBytes = Convert.FromBase64String(StoredSalt);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Password ?? "", SaltBytes, 10000, HashAlgorithmName.SHA256);

            var newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return newHash == StoredHash;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _context.UserInfo;
        }

        //GetAllUsersDataByUsername
        public UserModel GetAllUserDataByUsername(string username)
        {
            return _context.UserInfo.FirstOrDefault(user => user.Username == username);
        }

        public IActionResult Login(LoginDTO user)
        {
            IActionResult result = Unauthorized();
            //A check to see if the user exists.
            if (DoesUserExist(user.Username))
            {
                UserModel foundUser = GetAllUserDataByUsername(user.Username);
                if (VerifyUserPassword(user.Password, foundUser.Hash, foundUser.Salt))
                {
                    //Create a secret key used to sign the JTW token
                    //This should be stored securely (not hard coded in production)
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superdupersuperdupersuperdupersuperdupersecurekey@676767"));//Enter random characters until you hit the 256 characters.
                                                                                                                                                 //Create signing credentials using the secret key and HMACSHA256 algorithm
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256); //This ensures that the token can't be tampered with

                    //Build the JWT token with metadata.
                    var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost:5001",
                        audience: "https://localhost:5001",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signingCredentials
                    );

                    //Convert the token object into string that can be sent to the client
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    //Return the token as JSON to the client
                    result = Ok(new { Token = tokenString });
                }
            }
            //Return either the token if the user exists, or Unauthorized if the user does not exist.
            return result;
        }

        public UserIdDTO GetUserIdDTOByUserName(string username)
        {
            throw new NotImplementedException();
        }

        //Helper function to help us find a user
        public UserModel GetUserByUsername(string username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username);
        }

        public bool DeleteUser(string userToDelete)
        {
            UserModel foundUser = GetUserByUsername(userToDelete);
            bool result = false;

            if (foundUser != null)
            {
                foundUser.Username = userToDelete;
                _context.Remove(foundUser);

                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        //Another helper function to help us get user by id
        public UserModel GetUserById(int id)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Id == id);
        }

        public bool UpdateUser(int id, string username)
        {
            UserModel foundUser = GetUserById(id);
            bool result = false;
            if (foundUser != null)
            {
                foundUser.Username = username;
                _context.Update(foundUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }
    }
}