using Application.Common.Models.UserModels;
using Application.Common.Responses;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Secure.Hash.Algorithm.SDK.Controllers;
using Microsoft.AspNetCore.Http;
using Services.Auth;
using Microsoft.Extensions.Options;
using Application.Helper;
using Microsoft.AspNet.Identity;

namespace Application.Implementations
{
    public class ApplicationUser : IApplicationUser
    {
        private readonly ILogger<ApplicationUser> logger;
        private readonly IAccountUserDAO userDAO;
        private readonly ITokenization tokenization;
        private readonly IHttpContextAccessor httpContext;
        private readonly SecureData secureData;
        private readonly IOptions<JWTSettings> jtwOpt;
        private readonly PasswordHasherHelper _password;
        private readonly JWTSettings _jtwOpt;

        public ApplicationUser(ILogger<ApplicationUser> logger, IAccountUserDAO userDAO,
            ITokenization tokenization, IHttpContextAccessor httpContext, SecureData secureData,
            IOptions<JWTSettings> jtwOpt, PasswordHasherHelper passwordHasher)
        {
            this.logger = logger;
            this.userDAO = userDAO;
            this.tokenization = tokenization;
            this.httpContext = httpContext;
            this.secureData = secureData;
            this.jtwOpt = jtwOpt;
            _password = passwordHasher;
            _jtwOpt = jtwOpt.Value;
        }

        public async Task<ResponseModel<ApplicationUserResponse>> UserLogin(LoginUserModel model)
        {
            var response = new ResponseModel<ApplicationUserResponse>();
            try
            {
                var result = await userDAO.Login(model.Email, model.Password);
                if (result.Equals(PasswordVerificationResult.Success))
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
                    PasswordHash = _password.HashPassword(model.Password)
                };

                var result = await userDAO.CreateAccount(user);
                if (result)
                {
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
            SetJWTCookie(response.Data.Token);
            return response.Data;
        }
        private void SetJWTCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddHours(6),
            };

            httpContext.HttpContext.Response.Cookies.Append("X-Access-Token", token, cookieOptions);
        }
    }
}
