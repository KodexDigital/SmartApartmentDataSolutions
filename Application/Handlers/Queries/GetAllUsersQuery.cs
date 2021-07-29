using Application.Common.Models.Responses;
using Application.Common.Responses;
using Application.Interfaces;
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
        private readonly IAccountUserDAO userDAO;
        public GetAllUsersQueryHandler(IAccountUserDAO userDAO) //UserManager<IdentityUser> userManager
        {
            this.userDAO = userDAO;
        }
        public async Task<ResponseModel<List<UserListsResponseModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            //if (request.Access_Key != AppUtility.TemSecretKey)
            //    throw new CustomException("Invalid Access Code");

            var allUsers = await userDAO.GetAllUsers();
            return await Task.Run(() => new ResponseModel<List<UserListsResponseModel>>
            {
                Status = true,
                Message = $"{allUsers.ToList().Count} Record(s) Found!",
                Data = allUsers.Select(c => new UserListsResponseModel
                {
                    Username = c.Username,
                    PhoneNumber = c.PhoneNumber
                }).ToList()
            });
        }
    }
}
