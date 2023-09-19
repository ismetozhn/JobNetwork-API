using JobNetworkAPI.Application.DTOs.User;
using JobNetworkAPI.Domain.Entities.Identity;

namespace JobNetworkAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
    }
}
