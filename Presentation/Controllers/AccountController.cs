using Application.Attributes;
using Application.Common.Responses;
using Application.Handlers.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    [Produces(MediaTypeNames.Application.Json)]
    [AllowAnonymous]
    [Route("api/accounts")]
    [ModelValidation]
    public class AccountController : BaseEntryController
    {
        private readonly IHttpContextAccessor httpContext;
        public AccountController(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        /// <summary>
        /// User account creation endpoint
        /// </summary>
        /// <param name="command">The request payload.</param>
        /// <returns>Success if all payload is valid.</returns>
        [HttpPost("user-registration")]
        [ProducesResponseType(typeof(ResponseModel<ApplicationUserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel<ApplicationUserResponse>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationCommand command)
            => Ok(await Mediator.Send(command));

        /// <summary>
        /// User access gateway
        /// </summary>
        /// <param name="command">Request payloads.</param>
        /// <returns>Access resources.</returns>
        [HttpPost("sign-in")]
        [ProducesResponseType(typeof(ResponseModel<ApplicationUserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel<ApplicationUserResponse>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SignIn([FromBody] UserLoginCommand command)
             => Ok(await Mediator.Send(command));

        /// <summary>
        /// Access release endpoint
        /// </summary>
        /// <returns>Logout of the system.</returns>
        [HttpPost("sign-out")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult LogOut()
        {
            foreach (var cookie in httpContext.HttpContext.Request.Cookies)
                Response.Cookies.Delete(cookie.Key);
            return Ok(new ResponseModel { Status = true, Message = "User loggeed out successfully." });
        }
    }
}
