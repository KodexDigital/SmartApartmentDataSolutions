using Application.Common.Models.UserModels;
using Application.Common.Responses;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationUser
    {
        Task<ResponseModel<ApplicationUserResponse>> UserRegistration(ApplicationUserModel model);
        Task<ResponseModel<ApplicationUserResponse>> UserLogin(LoginUserModel model);
    }
}
