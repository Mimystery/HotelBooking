using HotelBooking.Domain.Models;

namespace HotelBooking.Infrastructure.Repositories;

public interface IUsersRepository
{
    Task<UserModel> GetUserByEmailAsync(string email);
    Task<UserModel> GetUserByIdAsync(Guid userId);
    Task AddUserAsync(UserModel userModel);
    Task DeleteUserAsync(Guid userId);
}