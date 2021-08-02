using Application.Common.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Services.Auth;
using SmartApp.DotNetCore.Services.CleintConfig;
using SmartApp.DotNetCore.Services.Interfaces;
using SmartApp.DotNetCore.Services.Requests;
using SmartApp.DotNetCore.Services.Responses;
using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartApp.DotNetCore.Services.Implementation
{
    public class SmartAppartmentService : ISmartAppartmentService
    {
        private readonly SmartAppService serviceConfig;
        private readonly HttpClient _client;
        private readonly ILogger<SmartAppartmentService> logger;
        private const string MEDIA_TYPE = MediaTypeNames.Application.Json;
        private readonly IHttpContextAccessor httpContext;
        private readonly JWTSettings _jtwOpt;

        public SmartAppartmentService(ILogger<SmartAppartmentService> logger, IOptions<SmartAppService> serviceOptions,
            IHttpClientFactory clientFactory, IHttpContextAccessor httpContext, IOptions<JWTSettings> jtwOpt)
        {
            serviceConfig = serviceOptions.Value;
            this.logger = logger;
            _client = clientFactory.CreateClient(serviceConfig.ClientName);
            this.httpContext = httpContext;
            _jtwOpt = jtwOpt.Value;
        }

        public async Task<LoginUserResponse> LoginUser(LoginUserRequest request)
        {
            var response = new LoginUserResponse();
            try
            {
                logger.LogInformation($"{nameof(LoginUser)} REQUEST INITIATED. THE REQUEST PAYLOAD => {request}");
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, MEDIA_TYPE);

                var result = await _client.PostAsync(serviceConfig.LoginEndpoint, content, CancellationToken.None);
                if (result.IsSuccessStatusCode)
                {
                    var responseResult = await result.Content.ReadAsStringAsync();
                    var jsonRespose = JsonConvert.DeserializeObject<LoginUserResponse>(responseResult);

                    if (jsonRespose.Status.Equals(true))
                    {
                        response.Status = jsonRespose.Status;
                        response.Message = jsonRespose.Message;
                        response.Data = jsonRespose.Data;
                    }
                    response.Status = jsonRespose.Status;
                    response.Message = jsonRespose.Message;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Client request failed";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message.ToString();
                logger.LogError($"The Response Payload ==> {response} with Errors ===> {ex.Message}");
                throw;
            }

            return response;
        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request)
        {
            var response = new RegisterUserResponse();
            try
            {
                logger.LogInformation($"{nameof(RegisterUser)} REQUEST INITIATED. THE REQUEST PAYLOAD => {request}");
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, MEDIA_TYPE);

                var result = await _client.PostAsync(serviceConfig.RegisterUserEndpoint, content, CancellationToken.None);
                if (result.IsSuccessStatusCode)
                {
                    var responseResult = await result.Content.ReadAsStringAsync();
                    var jsonRespose = JsonConvert.DeserializeObject<RegisterUserResponse>(responseResult);

                    if (jsonRespose.Status.Equals(true))
                    {
                        response.Status = jsonRespose.Status;
                        response.Data = jsonRespose.Data;
                        response.Message = jsonRespose.Message;
                    }
                    response.Status = jsonRespose.Status;
                    response.Message = jsonRespose.Message;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Client request failed";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message.ToString();
                logger.LogError($"The Response Payload ==> {response} with Errors ===> {ex.Message}");
                throw;
            }

            return response;
        }

        public async Task<GetAllUserResponse> GetAllUsers()
        {
            var response = new GetAllUserResponse();
            try
            {
                logger.LogInformation($"{nameof(RegisterUser)} REQUEST INITIATED. THE REQUEST PAYLOAD");
                var result = await _client.GetAsync(serviceConfig.GetAllUsersEndpoint, CancellationToken.None);
                if (result.IsSuccessStatusCode)
                {
                    var responseResult = await result.Content.ReadAsStringAsync();
                    var jsonRespose = JsonConvert.DeserializeObject<GetAllUserResponse>(responseResult);
                    if (jsonRespose.Status.Equals(true))
                    {
                        response.Status = jsonRespose.Status;
                        response.Data = jsonRespose.Data;
                        response.Message = jsonRespose.Message;
                    }
                    else
                    {
                        response.Status = jsonRespose.Status;
                        response.Message = jsonRespose.Message;
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "Client request failed";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message.ToString();
                logger.LogError($"The Response Payload ==> {response} with Errors ===> {ex.Message}");
                throw;
            }

            return response;
        }

        public async Task<ResponseModel> LoginOutUser()
        {
            var response = new ResponseModel();
            try
            {
                logger.LogInformation($"{nameof(LoginOutUser)} REQUEST INITIATED");

                var result = await _client.PostAsync(serviceConfig.LogoutEndpoint, null, CancellationToken.None);
                if (result.IsSuccessStatusCode)
                {
                    var responseResult = await result.Content.ReadAsStringAsync();
                    var jsonRespose = JsonConvert.DeserializeObject<ResponseModel>(responseResult);
                    if (jsonRespose.Status.Equals(true))
                    {
                        response.Status = jsonRespose.Status;
                        response.Message = jsonRespose.Message;
                    }
                    else
                    {
                        response.Status = jsonRespose.Status;
                        response.Message = jsonRespose.Message;
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "Client request failed";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message.ToString();
                logger.LogError($"The Response Payload ==> {response} with Errors ===> {ex.Message}");
                throw;
            }

            return response;
        }
    }
}
