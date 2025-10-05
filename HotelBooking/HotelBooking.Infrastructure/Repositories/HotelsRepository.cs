using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelBooking.Domain.Contracts.Hotels;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Repositories
{
    public class HotelsRepository : IHotelsRepository
    {
        private readonly HotelBookingDBContext _context;
        private readonly IMapper _mapper;

        public HotelsRepository(HotelBookingDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<HotelModel>> GetAllHotelsAsync()
        {
            var hotels = await _context.Hotels.Include(h => h.Rooms).ToListAsync();
            return _mapper.Map<List<HotelModel>>(hotels);
        }

        public async Task<HotelModel> GetHotelByIdAsync(Guid hotelId)
        {
            var hotel = await _context.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(h => h.HotelId == hotelId);

            if (hotel == null)
            {
                throw new KeyNotFoundException();
            }

            return _mapper.Map<HotelModel>(hotel);
        }

        public async Task AddHotelAsync(HotelModel hotelmodel)
        {
            var hotelEntity = _mapper.Map<HotelEntity>(hotelmodel);
            await _context.Hotels.AddAsync(hotelEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHotelAsync(Guid hotelId, HotelUpdateDTO hotelUpdateDto)
        {
            var existingHotel = await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId == hotelId);

            if (existingHotel == null)
            { 
                throw new KeyNotFoundException();
            }

            if (hotelUpdateDto.HotelName != null)
            {
                existingHotel.HotelName = hotelUpdateDto.HotelName;
            }
            if (hotelUpdateDto.Address != null)
            {
                existingHotel.Address = hotelUpdateDto.Address;
            }
            if (hotelUpdateDto.City != null)
            {
                existingHotel.City = hotelUpdateDto.City;
            }
            if (hotelUpdateDto.Description != null)
            {
                existingHotel.Description = hotelUpdateDto.Description;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteHotelAsync(Guid hotelId)
        {
            var existingHotel = await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId == hotelId);
            if (existingHotel == null)
            {
                throw new KeyNotFoundException();
            }
            _context.Hotels.Remove(existingHotel);
            await _context.SaveChangesAsync();
        }
    }
}
