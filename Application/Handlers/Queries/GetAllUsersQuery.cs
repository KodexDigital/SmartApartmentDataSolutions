using Application.Common.Models.Responses;
using Application.Common.Responses;
using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Queries
{
    public class GetAllUsersQuery : IRequest<ResponseModel<List<UserListsResponseModel>>>
    {}
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ResponseModel<List<UserListsResponseModel>>>
    {
        private readonly IAccountUserDAO userDAO;
        public GetAllUsersQueryHandler(IAccountUserDAO userDAO)
        {
            this.userDAO = userDAO;
        }
        public async Task<ResponseModel<List<UserListsResponseModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var allUsers = await userDAO.GetAllUsers();
            return await Task.Run(() => new ResponseModel<List<UserListsResponseModel>>
            {
                Status = true,
                Message = $"{allUsers.ToList().Count} Record(s) Found!",
                Data = allUsers.Select(c => new UserListsResponseModel
                {
                    Username = c.Username,
                    PhoneNumber = c.PhoneNumber,
                }).ToList()
            });
        }
    }
}
