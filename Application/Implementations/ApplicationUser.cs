using Application.Common.Models.UserModels;
using Application.Common.Responses;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public class ApplicationUser : IApplicationUser
    {
        private readonly ILogger<ApplicationUser> logger;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ITokenization tokenization;
        public ApplicationUser(ILogger<ApplicationUser> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            ITokenization tokenization)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenization = tokenization;
        }
        public async Task<ResponseModel<ApplicationUserResponse>> UserLogin(LoginUserModel model)
        {
            var response = new ResponseModel<ApplicationUserResponse>();
            try
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    response.Status = result.Succeeded;
                    response.Data = UserAuthResponse(model, response, user);

                    logger.LogInformation($"Login was successful with the following ==>{user} | {result} | {response}");
                }
                else
                {
                    response.Status = false;
                    response.Message = "Error occured while login in user";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message.ToString();
                logger.LogError($"Error exception RESPONSES ====> {response}");
                throw;
            }

            return response;
        }

        public async Task<ResponseModel<ApplicationUserResponse>> UserRegistration(ApplicationUserModel model)
        {
            var response = new ResponseModel<ApplicationUserResponse>();
            try
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = model.Password
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    response.Status = result.Succeeded;
                    response.Data = UserAuthResponse(model, response, user);

                    logger.LogInformation($"Registration was successful with the following ==>{user} | {result} | {response}");
                }
                else
                {
                    response.Status = false;
                    response.Message = "Error occured while creating user";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message.ToString();
                logger.LogError($"Error exception RESPONSES ====> {response}");
                throw;
            }

            return response;
        }

        private ApplicationUserResponse UserAuthResponse(dynamic model, ResponseModel<ApplicationUserResponse> response, IdentityUser user)
        {
            response.Data = new ApplicationUserResponse
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Token = tokenization.GetAccessToken(model.Email)
            };

            return response.Data;
        }
    }
}
