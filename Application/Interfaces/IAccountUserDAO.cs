using Application.Common.Models.Responses;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountUserDAO
    {
        Task<bool> CreateAccount(AppUser user);
        Task<object> IsUserExist(string username);
        Task<PasswordVerificationResult> Login(string username, string password);
        Task<string> HashedPassword(string username);
        Task<IEnumerable<UserListsResponseModel>> GetAllUsers();
    }
}
