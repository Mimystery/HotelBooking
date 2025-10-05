using HotelBooking.Domain.Contracts.Bookings;
using HotelBooking.Domain.Models;

namespace HotelBooking.Infrastructure.Repositories;

public interface IBookingsRepository
{
    Task<List<BookingModel>> GetAllBookingsAsync();
    Task<List<BookingModel>> GetBookingsByUserIdAsync(Guid userId);
    Task<List<BookingModel>> GetBookingsByRoomIdAsync(Guid roomId);
    Task<List<BookingsStatisticsDTO>> GetBookingsStatisticsAsync();
    Task AddBookingAsync(BookingModel bookingModel);
    Task UpdateBookingAsync(Guid bookingId, BookingUpdateDTO bookingUpdateDto);
    Task DeleteBookingAsync(Guid bookingId);
}