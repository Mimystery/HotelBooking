using HotelBooking.Application.Identity.Authentication.AuthContracts;
using HotelBooking.Domain.Contracts.Users;

namespace HotelBooking.Application.Identity.Authentication.Interfaces;

public interface IIdentityService
{
    Task Register(UserRegisterDTO userRegisterDto);
    Task<TokenDTO> Login(UserLoginDTO userLoginDto);
    Task<TokenDTO> RefreshToken(string refreshToken);
}