using Application.Attributes;
using Application.Common.Responses;
using Application.Handlers.Commands;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("user-registration")]
        [ProducesResponseType(typeof(ResponseModel<ApplicationUserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel<ApplicationUserResponse>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationCommand command)
            => Ok(await Mediator.Send(command));

        [HttpPost("sign-in")]
        [ProducesResponseType(typeof(ResponseModel<ApplicationUserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel<ApplicationUserResponse>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SignIn([FromBody] UserLoginCommand command)
             => Ok(await Mediator.Send(command));
    }
}
