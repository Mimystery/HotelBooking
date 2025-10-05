using HotelBooking.Domain.Contracts.Hotels;
using HotelBooking.Domain.Models;

namespace HotelBooking.Infrastructure.Repositories;

public interface IHotelsRepository
{
    Task<List<HotelModel>> GetAllHotelsAsync();
    Task<HotelModel> GetHotelByIdAsync(Guid hotelId);
    Task AddHotelAsync(HotelModel hotelEntity);
    Task UpdateHotelAsync(Guid hotelId, HotelUpdateDTO hotelUpdateDto);
    Task DeleteHotelAsync(Guid hotelId);
}