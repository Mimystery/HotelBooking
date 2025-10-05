using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Identity.Authentication.Interfaces;

public interface IRefreshTokenService
{
    Task<RefreshTokenModel> GetRefreshTokenByTokenAsync(string token);
    Task<RefreshTokenModel> GetRefreshTokenByUserIdAsync(Guid userId);
    Task AddRefreshTokenAsync(Guid userId, string refreshToken);
    Task DeleteRefreshTokenAsync(string token);
}