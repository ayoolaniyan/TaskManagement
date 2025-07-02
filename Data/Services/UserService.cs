using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.ViewModels;
using TaskManagement.Models;

namespace TaskManagement.Data.Services
{
    public class UserService
    {
        private AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public void AddUser(UserVM user)
        {
            if (_context.Users.Any(u => u.Name == user.Name))
                throw new InvalidOperationException("Name already exist.");

            var _user = new User()
            {
                Name = user.Name
            };
            _context.Users.Add(_user);
            _context.SaveChanges();
        }

        public List<User> GetUsers() => [.. _context.Users.Include(u => u.Tasks)];
    }
}
