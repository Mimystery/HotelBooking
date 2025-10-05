using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Repositories
{
    public class RefreshTokensRepository : IRefreshTokensRepository
    {
        private readonly HotelBookingDBContext _context;
        private readonly IMapper _mapper;

        public RefreshTokensRepository(HotelBookingDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RefreshTokenModel> GetByTokenAsync(string token)
        {
            var refreshTokenEntity = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token);

            if (refreshTokenEntity == null)
            {
                throw new KeyNotFoundException("Refresh token not found.");
            }

            return _mapper.Map<RefreshTokenModel>(refreshTokenEntity);
        }

        public async Task<RefreshTokenModel> GetTokenByUserIdAsync(Guid userId)
        {
            var refreshTokenEntity = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.UserId == userId);

            return _mapper.Map<RefreshTokenModel>(refreshTokenEntity);
        }

        public async Task AddAsync(RefreshTokenModel refreshTokenModel)
        {
            var refreshTokenEntity = _mapper.Map<RefreshTokenEntity>(refreshTokenModel);
            await _context.RefreshTokens.AddAsync(refreshTokenEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string token)
        {
            var refreshTokenEntity = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token);

            if (refreshTokenEntity == null)
            {
                throw new KeyNotFoundException("Refresh token not found.");
            }

            _context.RefreshTokens.Remove(refreshTokenEntity);
            await _context.SaveChangesAsync();
        }
    }
}
