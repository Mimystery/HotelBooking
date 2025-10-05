using HotelBooking.Domain.Contracts.Rooms;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Services;

public interface IRoomsService
{
    Task<List<RoomModel>> GetAllRoomsAsync();
    Task<RoomModel> GetRoomByIdAsync(Guid roomId);
    Task<Guid> AddRoomAsync(RoomCreateDTO roomCreateDto);
    Task UpdateRoomAsync(Guid roomId, RoomUpdateDTO roomUpdateDto);
    Task DeleteRoomAsync(Guid roomId);
}