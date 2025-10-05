using HotelBooking.Domain.Contracts.Bookings;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Services;

public interface IBookingsService
{
    Task<List<BookingModel>> GetAllBookingsAsync();
    Task<List<BookingModel>> GetBookingsByUserIdAsync(Guid userId);
    Task<List<BookingModel>> GetBookingsByRoomIdAsync(Guid roomId);
    Task<List<BookingsStatisticsDTO>> GetBookingsStatisticsAsync();
    Task AddBookingAsync(BookingCreateDTO bookingCreateDto);
    Task UpdateBookingAsync(Guid bookingId, BookingUpdateDTO bookingUpdateDto);
    Task DeleteBookingAsync(Guid bookingId);
}