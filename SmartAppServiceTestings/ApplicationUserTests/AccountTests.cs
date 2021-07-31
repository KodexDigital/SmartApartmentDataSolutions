using Application.Common.Models.UserModels;
using Application.Common.Responses;
using Application.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace SmartAppServiceTestings.ApplicationUserTests
{
    public class AccountTests
    {
        readonly ResponseModel response;
        readonly Mock<IApplicationUser> appUser;
        public AccountTests()
        {
            appUser = new Mock<IApplicationUser>();
            response  = new ResponseModel();
        }

        [Fact(DisplayName = "New user registration")]
        public void REGISTER_NEW_USER()
        {
            var userModel = new ApplicationUserModel
            {
                Email = "Kenth@gmail.com",
                Password = "",
                PhoneNumber = "0787842122"
            };

            var res = new ResponseModel<ApplicationUserResponse>
            {
                Status = true,
                Message = "User registered successfully"
            };

            response.Status = true;
            appUser.Setup(a => a.UserRegistration(userModel)).Returns(Task.FromResult(res));
         }
    }
}
