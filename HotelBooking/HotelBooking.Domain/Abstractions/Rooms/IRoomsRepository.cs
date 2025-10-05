using HotelBooking.Domain.Contracts.Rooms;
using HotelBooking.Domain.Models;

namespace HotelBooking.Infrastructure.Repositories;

public interface IRoomsRepository
{
    Task<List<RoomModel>> GetAllRoomsAsync();
    Task<RoomModel> GetRoomByIdAsync(Guid roomId);
    Task AddRoomAsync(RoomModel roomModel);
    Task UpdateRoomAsync(Guid roomId, RoomUpdateDTO roomUpdateDto);
    Task DeleteRoomAsync(Guid roomId);
}