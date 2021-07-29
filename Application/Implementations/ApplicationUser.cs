using Application.Common.Models.UserModels;
using Application.Common.Responses;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public class ApplicationUser : IApplicationUser
    {
        private readonly ILogger<ApplicationUser> logger;
        private readonly IAccountUserDAO userDAO;
        private readonly ITokenization tokenization;
        public ApplicationUser(ILogger<ApplicationUser> logger, IAccountUserDAO userDAO,
            ITokenization tokenization)
        {
            this.logger = logger;
            this.userDAO = userDAO;
            this.tokenization = tokenization;
        }

        public async Task<ResponseModel<ApplicationUserResponse>> UserLogin(LoginUserModel model)
        {
            var response = new ResponseModel<ApplicationUserResponse>();
            try
            {
                var result = await userDAO.IsUserExist(model.Email, model.Password);
                if (result != null)
                {
                    response.Status = true;
                    response.Message = "Login successfull";
                    response.Data = UserAuthResponse(model, response, new AppUser { Email = model.Email });

                    logger.LogInformation($"Login was successful for user ==>{model.Email} | {result} | {response}");
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
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = model.Password
                };

                var result = await userDAO.CreateAccount(user);

                //var result = await userManager.CreateAsync(user, model.Password);
                if (result)
                {
                    //await signInManager.SignInAsync(user, isPersistent: false);
                    response.Status = true;
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

        private ApplicationUserResponse UserAuthResponse(dynamic model, ResponseModel<ApplicationUserResponse> response, AppUser user)
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
