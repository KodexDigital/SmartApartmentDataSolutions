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
        //private string GetIdToken(IdentityUser user)
        //{
        //    var payload = new Dictionary<string, object>
        //    {
        //        { "id", user.Id },
        //        { "sub", user.Email },
        //        { "email", user.Email },
        //        { "emailConfirmed", user.EmailConfirmed },
        //    };
        //    return GetToken(payload);
        //}

        //private string GetAccessToken(string Email)
        //{
        //    var payload = new Dictionary<string, object>
        //    {
        //        { "sub", Email },
        //        { "email", Email }
        //    };
        //    return GetToken(payload);
        //}

        //private string GetToken(Dictionary<string, object> payload)
        //{
        //    //var secret = jWTOption.Key;

        //    //payload.Add("iss", jWTOption.Issuer);
        //    //payload.Add("aud", jWTOption.Issuer);
        //    //payload.Add("nbf", ConvertToUnixTimestamp(DateTime.Now));
        //    //payload.Add("iat", ConvertToUnixTimestamp(DateTime.Now));
        //    //payload.Add("exp", ConvertToUnixTimestamp(DateTime.Now.AddDays(7)));
        //    //IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        //    //IJsonSerializer serializer = new JsonNetSerializer();
        //    //IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        //    //IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

        //    //return encoder.Encode(payload, secret);

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTOption.SecretKey));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(jWTOption.Issuer, jWTOption.Issuer,
        //      expires: DateTime.Now.AddMinutes(30),
        //      signingCredentials: creds);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        ////private JsonResult Errors(IdentityResult result)
        ////{
        ////    var items = result.Errors
        ////        .Select(x => x.Description).ToArray();
        ////    return new JsonResult(items) { StatusCode = 400 };
        ////}

        //private JsonResult Error(string message)
        //{
        //    return new JsonResult(message) { StatusCode = 400 };
        //}

        ////private static double ConvertToUnixTimestamp(DateTime date)
        ////{
        ////    DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        ////    TimeSpan diff = date.ToUniversalTime() - origin;
        ////    return Math.Floor(diff.TotalSeconds);
        ////}
    }
}
