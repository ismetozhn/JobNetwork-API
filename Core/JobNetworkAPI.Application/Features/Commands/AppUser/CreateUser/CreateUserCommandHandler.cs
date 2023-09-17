using JobNetworkAPI.Application.Abstractions.Services;
using JobNetworkAPI.Application.DTOs.User;
using JobNetworkAPI.Application.Exceptions;
using JobNetworkAPI.Application.Features.Commands.AppUser.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobNetworkAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            //IdentityResult result = await _userManager.CreateAsync(new()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    UserName = request.surname,
            //    Email = request.email,
            //    NameSurname = request.name,
            //    PhoneNumber = request.contactNumber
            //}, request.password);

            //CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };

            //if (result.Succeeded)
            //    response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            //else
            //    foreach (var error in result.Errors)
            //        response.Message += $"{error.Code} - {error.Description}\n";

            CreateUserResponse response = await _userService.CreateAsync(new()
            {
                email = request.email,
                password = request.password,
                name = request.name,
                passwordConfirm = request.passwordConfirm,
                surname = request.surname,
                contactNumber = request.contactNumber,
            });

            return new() 
            { 
                Message= response.Message,
                Succeeded= response.Succeeded,
            };

            //throw new UserCreateFailedException();
        }
    }
}