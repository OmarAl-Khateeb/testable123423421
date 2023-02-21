using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructue.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreContext _context;
        public UserRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<User>> GetUsersAsync()
        {
            return await _context.Users
                .Include(p => p.UserType)
                .ToListAsync();
        }        
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(p => p.UserType)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<UserType>> GetUserTypesAsync()
        {
            return await _context.UserTypes.ToListAsync();
        }

        public Task<IReadOnlyList<UserAttachment>> GetUserAttachmentsAsync()
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }


    }
}