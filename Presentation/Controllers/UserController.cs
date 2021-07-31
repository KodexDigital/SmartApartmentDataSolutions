using Application.Common.Models.Responses;
using Application.Common.Responses;
using Application.Handlers.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/users")]
    [Authorize]
    public class UserController : BaseEntryController
    {

        /// <summary>
        /// The endpoint to view all registered users
        /// </summary>
        /// <returns>List of all users.</returns>
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseModel<UserListsResponseModel>), (int)HttpStatusCode.OK)]
        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
            => Ok(await Mediator.Send(new GetAllUsersQuery { })); 
    }
}