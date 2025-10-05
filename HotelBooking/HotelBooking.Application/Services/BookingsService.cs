using AutoMapper;
using HotelBooking.Domain.Contracts.Bookings;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Services
{
    public class BookingsService : IBookingsService
    {
        private readonly IBookingsRepository _bookingsRepository;
        private readonly IMapper _mapper;

        public BookingsService(IBookingsRepository bookingsRepository, IMapper mapper)
        {
            _bookingsRepository = bookingsRepository;
            _mapper = mapper;
        }

        public async Task<List<BookingModel>> GetAllBookingsAsync()
        {
            return await _bookingsRepository.GetAllBookingsAsync();
        }

        public async Task<List<BookingModel>> GetBookingsByUserIdAsync(Guid userId)
        {
            return await _bookingsRepository.GetBookingsByUserIdAsync(userId);
        }

        public async Task<List<BookingModel>> GetBookingsByRoomIdAsync(Guid roomId)
        {
            return await _bookingsRepository.GetBookingsByRoomIdAsync(roomId);
        }

        public async Task<List<BookingsStatisticsDTO>> GetBookingsStatisticsAsync()
        {
            return await _bookingsRepository.GetBookingsStatisticsAsync();
        }

        public async Task AddBookingAsync(BookingCreateDTO bookingCreateDto)
        {
            var bookingModel = new BookingModel(
                Guid.NewGuid(),
                bookingCreateDto.UserId,
                bookingCreateDto.RoomId,
                bookingCreateDto.HotelId,
                bookingCreateDto.HotelName,
                bookingCreateDto.City,
                bookingCreateDto.RoomName,
                bookingCreateDto.FirstName,
                bookingCreateDto.LastName,
                bookingCreateDto.CheckInDate,
                bookingCreateDto.CheckOutDate,
                bookingCreateDto.TotalPrice,
                DateTime.UtcNow
            );

            await _bookingsRepository.AddBookingAsync(bookingModel);
        }

        public async Task UpdateBookingAsync(Guid bookingId, BookingUpdateDTO bookingUpdateDto)
        {
            await _bookingsRepository.UpdateBookingAsync(bookingId, bookingUpdateDto);
        }

        public async Task DeleteBookingAsync(Guid bookingId)
        {
            await _bookingsRepository.DeleteBookingAsync(bookingId);
        }
    }
}
