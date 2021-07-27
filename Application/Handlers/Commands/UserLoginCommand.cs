using Application.Common.Exceptions;
using Application.Common.Models.UserModels;
using Application.Common.Responses;
using Application.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Commands
{
    public class UserLoginCommand : LoginUserModel, IRequest<ResponseModel>
    { }
    public class UserSignInCommandValidator : AbstractValidator<UserLoginCommand>
    {
        public UserSignInCommandValidator()
        {
            RuleFor(c => c.Email).EmailAddress().NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
        }
    }
    public class UserSignInCommandHandler : IRequestHandler<UserLoginCommand, ResponseModel>
    {
        private readonly IApplicationUser appUser;
        public UserSignInCommandHandler(IApplicationUser appUser)
        {
            this.appUser = appUser;
        }
        public async Task<ResponseModel> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request != null)
                {
                    var result = await appUser.UserLogin(request);
                    if (result.Status.Equals(true))
                        return ResponseModel<ApplicationUserResponse>.Success(result.Data, result.Message);
                    return ResponseModel.Failure(result.Message);
                }
                return ResponseModel.Failure("Request is empty... Failed signing-in.");
            }
            catch (Exception ex) { throw new CustomException($"An error occured with description -> {ex}"); }
        }
    }
}
