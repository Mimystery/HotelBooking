using AutoMapper;
using HotelBooking.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Domain.Models;

namespace HotelBooking.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly HotelBookingDBContext _context;
        private readonly IMapper _mapper;

        public UsersRepository(HotelBookingDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> GetUserByIdAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                throw new KeyNotFoundException();
            }
            return _mapper.Map<UserModel>(user);
        }

        public async Task AddUserAsync(UserModel userModel)
        {
            var userEntity = _mapper.Map<UserEntity>(userModel);
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                throw new KeyNotFoundException();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
