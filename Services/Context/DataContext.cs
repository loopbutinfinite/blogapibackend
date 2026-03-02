using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogapibackend.Models;
using Microsoft.EntityFrameworkCore;

namespace blogapibackend.Services.Context
{
        //This will help us bridge our backend C# code to our database since the database only understands SQL.
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<UserModel> UserInfo {get;set;}
        public DbSet<BlogItemModel> BlogInfo {get;set;}
    }
}