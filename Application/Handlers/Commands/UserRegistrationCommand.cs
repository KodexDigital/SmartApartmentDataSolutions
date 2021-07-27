using Application.Common.Exceptions;
using Application.Common.Models.UserModels;
using Application.Common.Responses;
using Application.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Commands
{
    public class UserRegistrationCommand : IRequest<ResponseModel>
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
    public class UserSignUpCommandValidator : AbstractValidator<UserRegistrationCommand>
    {
        public UserSignUpCommandValidator()
        {
            RuleFor(c => c.PhoneNumber).NotEmpty();
            RuleFor(c => c.Email).EmailAddress().NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.ConfirmPassword).NotEmpty();
        }
    }
    public class UserSignUpCommandHandler : IRequestHandler<UserRegistrationCommand, ResponseModel>
    {
        private readonly IApplicationUser appUser;
        public UserSignUpCommandHandler(IApplicationUser appUser)
        {
            this.appUser = appUser;
        }
        public async Task<ResponseModel> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request != null)
                {
                    if (!request.Password.Equals(request.ConfirmPassword))
                        throw new CustomException("Password mis-matched");

                    var result = await appUser.UserRegistration(new ApplicationUserModel
                    {
                        PhoneNumber = request.PhoneNumber,
                        Email = request.Email,
                        Password = request.Password
                    });
                    if (result.Status.Equals(true))
                        return ResponseModel<ApplicationUserResponse>.Success(result.Data, result.Message);
                    return ResponseModel.Failure(result.Message);
                }
                return ResponseModel.Failure("Request is empty...");
            }
            catch (Exception ex) { throw new CustomException($"An error occured with description -> {ex}"); }
        }
    }
}
