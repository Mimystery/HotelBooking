using AutoMapper;
using HotelBooking.Domain.Contracts.Rooms;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Services
{
    public class RoomsService : IRoomsService
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly IMapper _mapper;

        public RoomsService(IRoomsRepository roomsRepository, IMapper mapper)
        {
            _roomsRepository = roomsRepository;
            _mapper = mapper;
        }

        public async Task<List<RoomModel>> GetAllRoomsAsync()
        {
            return await _roomsRepository.GetAllRoomsAsync();
        }

        public async Task<RoomModel> GetRoomByIdAsync(Guid roomId)
        {
            return await _roomsRepository.GetRoomByIdAsync(roomId);
        }

        public async Task<Guid> AddRoomAsync(RoomCreateDTO roomCreateDto)
        {
            var roomModel = new RoomModel(
                Guid.NewGuid(),
                roomCreateDto.HotelId,
                roomCreateDto.RoomName,
                roomCreateDto.RoomSize,
                roomCreateDto.PricePerNight
            );

            await _roomsRepository.AddRoomAsync(roomModel);
            return roomModel.RoomId;
        }

        public async Task UpdateRoomAsync(Guid roomId, RoomUpdateDTO roomUpdateDto)
        {
            await _roomsRepository.UpdateRoomAsync(roomId, roomUpdateDto);
        }

        public async Task DeleteRoomAsync(Guid roomId)
        {
            await _roomsRepository.DeleteRoomAsync(roomId);
        }
    }
}
