using HotelBooking.Domain.Contracts.Hotels;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Services;

public interface IHotelsService
{
    Task<List<HotelModel>> GetAllHotelsAsync();
    Task<HotelModel> GetHotelByIdAsync(Guid hotelId);
    Task<Guid> AddHotelAsync(HotelCreateDTO hotelCreateDto);
    Task UpdateHotelAsync(Guid hotelId, HotelUpdateDTO hotelUpdateDto);
    Task DeleteHotelAsync(Guid hotelId);
}