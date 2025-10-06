using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelBooking.Application.Identity.Authentication.AuthContracts;
using HotelBooking.Application.Identity.Authentication.Interfaces;
using HotelBooking.Domain.Contracts.Users;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Repositories;

namespace HotelBooking.Application.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IRefreshTokenService _refreshTokenService;

        public IdentityService(IUsersRepository usersRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IMapper mapper, IRefreshTokenService refreshTokenService)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _refreshTokenService = refreshTokenService;
        }

        public async Task Register(UserRegisterDTO userRegisterDto)
        {
            var hashedPassword = _passwordHasher.Generate(userRegisterDto.Password);

            var userWithEmail = await _usersRepository.GetUserByEmailAsync(userRegisterDto.Email);
            if (userWithEmail != null)
            {
                throw new InvalidOperationException("Email is already in use.");
            }

            var userModel = new UserModel(
                Guid.NewGuid(),
                userRegisterDto.Username,
                hashedPassword,
                userRegisterDto.FirstName,
                userRegisterDto.LastName,
                userRegisterDto.Email,
                userRegisterDto.Role
            );
            await _usersRepository.AddUserAsync(userModel);
        }
        public async Task<TokenDTO> Login(UserLoginDTO userLoginDto)
        {
            var user = await _usersRepository.GetUserByEmailAsync(userLoginDto.Email);

            if (user == null || !_passwordHasher.Verify(userLoginDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var existingRefreshToken = await _refreshTokenService.GetRefreshTokenByUserIdAsync(user.UserId);

            if (existingRefreshToken != null)
            {
                await _refreshTokenService.DeleteRefreshTokenAsync(existingRefreshToken.Token);
            }

            var token = _jwtProvider.GenerateToken(user);
            var refreshToken = _jwtProvider.GenerateRefreshToken();

            await _refreshTokenService.AddRefreshTokenAsync(user.UserId, refreshToken);

            return new TokenDTO
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenDTO> RefreshToken(string refreshToken)
        {
            var storedToken = await _refreshTokenService.GetRefreshTokenByTokenAsync(refreshToken);

            if (storedToken == null || storedToken.Expires < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");
            }

            var user = await _usersRepository.GetUserByIdAsync(storedToken.UserId);

            var newAccessToken = _jwtProvider.GenerateToken(user);
            var newRefreshToken = _jwtProvider.GenerateRefreshToken();

            await _refreshTokenService.DeleteRefreshTokenAsync(storedToken.Token);
            await _refreshTokenService.AddRefreshTokenAsync(user.UserId, newRefreshToken);

            return new TokenDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
