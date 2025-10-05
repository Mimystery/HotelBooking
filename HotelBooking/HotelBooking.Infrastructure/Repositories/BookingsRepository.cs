using AutoMapper;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.Domain.Contracts.Bookings;

namespace HotelBooking.Infrastructure.Repositories
{
    public class BookingsRepository : IBookingsRepository
    {
        private readonly HotelBookingDBContext _context;
        private readonly IMapper _mapper;

        public BookingsRepository(HotelBookingDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BookingModel>> GetAllBookingsAsync()
        {
            var bookings = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Room)
                .Include(b => b.Hotel)
                .ToListAsync();

            return _mapper.Map<List<BookingModel>>(bookings);
        }

        public async Task<List<BookingModel>> GetBookingsByUserIdAsync(Guid userId)
        {
            var bookings = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Room)
                .Include(b => b.Hotel)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<BookingModel>>(bookings);
        }

        public async Task<List<BookingModel>> GetBookingsByRoomIdAsync(Guid roomId)
        {
            var bookings = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Room)
                .Include(b => b.Hotel)
                .Where(b => b.RoomId == roomId)
                .ToListAsync();

            return _mapper.Map<List<BookingModel>>(bookings);
        }

        public async Task<List<BookingsStatisticsDTO>> GetBookingsStatisticsAsync()
        {
            var last7Days = DateTime.UtcNow.AddDays(-7);

            var bookings = await _context.Bookings
                .Where(b => b.CreatedAt >= last7Days)
                .GroupBy(b => b.CreatedAt.Date)
                .Select(g => new BookingsStatisticsDTO
                {
                    Date = g.Key,
                    Count = g.Count()
                }).ToListAsync();

            return bookings;
        }

        public async Task AddBookingAsync(BookingModel bookingModel)
        {
            var bookingEntity = _mapper.Map<BookingEntity>(bookingModel);
            await _context.Bookings.AddAsync(bookingEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookingAsync(Guid bookingId, BookingUpdateDTO bookingUpdateDto)
        {
            var existingBooking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);
            if (existingBooking == null)
                throw new KeyNotFoundException();

            if (bookingUpdateDto.FirstName != null)
            {
                existingBooking.FirstName = bookingUpdateDto.FirstName;
            }

            if (bookingUpdateDto.LastName != null)
            {
                existingBooking.LastName = bookingUpdateDto.LastName;
            }

            if (bookingUpdateDto.CheckInDate != null)
            {
                existingBooking.CheckInDate = bookingUpdateDto.CheckInDate.Value;
            }

            if (bookingUpdateDto.CheckOutDate != null)
            {
                existingBooking.CheckOutDate = bookingUpdateDto.CheckOutDate.Value;
            }

            if (bookingUpdateDto.TotalPrice != null)
            {
                existingBooking.TotalPrice = bookingUpdateDto.TotalPrice.Value;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(Guid bookingId)
        {
            var existingBooking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);
            if (existingBooking == null)
                throw new KeyNotFoundException();

            _context.Bookings.Remove(existingBooking);
            await _context.SaveChangesAsync();
        }
    }
}
