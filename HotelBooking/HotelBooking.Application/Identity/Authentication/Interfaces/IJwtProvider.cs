using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Identity.Authentication.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(UserModel user);
    string GenerateRefreshToken();
}