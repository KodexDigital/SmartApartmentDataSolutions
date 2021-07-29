using Application.Common.Models.Responses;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountUserDAO
    {
        Task<bool> CreateAccount(AppUser user);
        Task<object> IsUserExist(string username, string password);
        Task<IEnumerable<UserListsResponseModel>> GetAllUsers();
    }
}
