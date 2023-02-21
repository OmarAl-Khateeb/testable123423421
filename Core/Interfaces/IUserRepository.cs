using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IReadOnlyList<User>> GetUsersAsync();
        Task<IReadOnlyList<UserAttachment>> GetUserAttachmentsAsync();
        Task<IReadOnlyList<UserType>> GetUserTypesAsync();

        void AddUser(User user);
    }
}