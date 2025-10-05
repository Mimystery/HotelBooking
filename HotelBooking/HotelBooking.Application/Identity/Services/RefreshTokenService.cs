using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelBooking.Application.Identity.Authentication.Interfaces;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Repositories;

namespace HotelBooking.Application.Identity.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly IMapper _mapper;

        public RefreshTokenService(IRefreshTokensRepository refreshTokensRepository, IMapper mapper)
        {
            _refreshTokensRepository = refreshTokensRepository;
            _mapper = mapper;
        }

        public async Task<RefreshTokenModel> GetRefreshTokenByTokenAsync(string token)
        {
            return await _refreshTokensRepository.GetByTokenAsync(token);
        }

        public async Task<RefreshTokenModel> GetRefreshTokenByUserIdAsync(Guid userId)
        {
            return await _refreshTokensRepository.GetTokenByUserIdAsync(userId);
        }
        public async Task AddRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var token = new RefreshTokenModel
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokensRepository.AddAsync(token);
        }

        public async Task DeleteRefreshTokenAsync(string token)
        {
            await _refreshTokensRepository.DeleteAsync(token);
        }
    }
}
