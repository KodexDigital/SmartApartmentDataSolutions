using Application.Common.Models.Responses;
using Application.Common.Responses;
using Application.Handlers.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/users")]
    public class UserController : BaseEntryController
    {

        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseModel<UserListsResponseModel>), (int)HttpStatusCode.OK)]
        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers() //[FromHeader] string access_token
            => Ok(await Mediator.Send(new GetAllUsersQuery { }));

    }
}

//var command = new GetAllUsersQuery { }; // Access_Key = access_token