using AutoMapper;
using HotelBooking.Application.Services;
using HotelBooking.Domain.Contracts.Rooms;
using HotelBooking.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomsService _roomsService;
        private readonly IMapper _mapper;

        public RoomsController(IRoomsService roomsService, IMapper mapper)
        {
            _roomsService = roomsService;
            _mapper = mapper;
        }

        [HttpGet("allRooms")]
        public async Task<ActionResult<List<RoomModel>>> GetAllRooms()
        {
            var rooms = await _roomsService.GetAllRoomsAsync();
            return Ok(rooms);
        }

        [HttpGet("roomById/{roomId}")]
        public async Task<ActionResult<RoomModel>> GetRoomById(Guid roomId)
        {
            try
            {
                var room = await _roomsService.GetRoomByIdAsync(roomId);
                return Ok(room);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addRoom")]
        public async Task<ActionResult> AddRoom([FromBody] RoomCreateDTO roomCreateDto)
        {
            var roomId = await _roomsService.AddRoomAsync(roomCreateDto);
            return Ok(new { roomId });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateRoom/{roomId}")]
        public async Task<ActionResult> UpdateRoom(Guid roomId, [FromBody] RoomUpdateDTO roomUpdateDto)
        {
            try
            {
                await _roomsService.UpdateRoomAsync(roomId, roomUpdateDto);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteRoom/{roomId}")]
        public async Task<ActionResult> DeleteRoom(Guid roomId)
        {
            try
            {
                await _roomsService.DeleteRoomAsync(roomId);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
