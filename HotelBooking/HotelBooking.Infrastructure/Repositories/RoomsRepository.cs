using AutoMapper;
using HotelBooking.Domain.Contracts.Rooms;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Repositories
{
    public class RoomsRepository : IRoomsRepository
    {
        private readonly HotelBookingDBContext _context;
        private readonly IMapper _mapper;

        public RoomsRepository(HotelBookingDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<RoomModel>> GetAllRoomsAsync()
        {
            var rooms = await _context.Rooms.Include(r => r.Hotel).ToListAsync();
            return _mapper.Map<List<RoomModel>>(rooms);
        }

        public async Task<RoomModel> GetRoomByIdAsync(Guid roomId)
        {
            var room = await _context.Rooms.Include(r => r.Hotel).FirstOrDefaultAsync(r => r.RoomId == roomId);

            if (room == null)
            {
                throw new KeyNotFoundException();
            }

            return _mapper.Map<RoomModel>(room);
        }

        public async Task AddRoomAsync(RoomModel roomModel)
        {
            var roomEntity = _mapper.Map<RoomEntity>(roomModel);
            await _context.Rooms.AddAsync(roomEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoomAsync(Guid roomId, RoomUpdateDTO roomUpdateDto)
        {
            var existingRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId);
            if (existingRoom == null)
            {
                throw new KeyNotFoundException();
            }

            if (roomUpdateDto.RoomName != null)
            {
                existingRoom.RoomName = roomUpdateDto.RoomName;
            }
            if (roomUpdateDto.RoomSize != null)
            {
                existingRoom.RoomSize = roomUpdateDto.RoomSize.Value;
            }
            if (roomUpdateDto.PricePerNight != null)
            {
                existingRoom.PricePerNight = roomUpdateDto.PricePerNight.Value;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoomAsync(Guid roomId)
        {
            var existingRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId);
            if (existingRoom == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Rooms.Remove(existingRoom);
            await _context.SaveChangesAsync();
        }
    }
}
