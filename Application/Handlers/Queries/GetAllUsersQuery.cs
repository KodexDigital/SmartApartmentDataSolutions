using Application.Common.Models.Responses;
using Application.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Queries
{
    public class GetAllUsersQuery : IRequest<ResponseModel<List<UserListsResponseModel>>>
    {
        //[JsonIgnore]
        //public string Access_Key { get; set; }

    }
    //public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
    //{
    //    public GetAllUsersQueryValidator()
    //    {
    //        RuleFor(c => c.Access_Key).NotEmpty();
    //    }

    //}
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ResponseModel<List<UserListsResponseModel>>>
    {
        private readonly UserManager<IdentityUser> userManager;

        public GetAllUsersQueryHandler(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<ResponseModel<List<UserListsResponseModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            //if (request.Access_Key != AppUtility.TemSecretKey)
            //    throw new CustomException("Invalid Access Code");

            var allUsers = userManager.Users.ToList();
            return await Task.Run(() => new ResponseModel<List<UserListsResponseModel>>
            {
                Data = allUsers.Select(c => new UserListsResponseModel
                {
                    Status = true,
                    Message = $"{allUsers.Count} Record(s) Found!",
                    Username = c.Email,
                    PhoneNumber = c.PhoneNumber
                }).ToList()
            });
        }
    }
}
