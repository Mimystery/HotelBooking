using HotelBooking.Domain.Models;

namespace HotelBooking.Infrastructure.Repositories;

public interface IRefreshTokensRepository
{
    Task<RefreshTokenModel> GetByTokenAsync(string token);
    Task<RefreshTokenModel> GetTokenByUserIdAsync(Guid userId);
    Task AddAsync(RefreshTokenModel refreshTokenModel);
    Task DeleteAsync(string token);
}