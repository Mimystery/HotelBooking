using AutoMapper;
using HotelBooking.Application.Services;
using HotelBooking.Domain.Contracts.Bookings;
using HotelBooking.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingsService _bookingsService;
        private readonly IMapper _mapper;

        public BookingsController(IBookingsService bookingsService, IMapper mapper)
        {
            _bookingsService = bookingsService;
            _mapper = mapper;
        }

        [HttpGet("allBookings")]
        public async Task<ActionResult<List<BookingModel>>> GetAllBookings()
        {
            var bookings = await _bookingsService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("userBookings/{userId}")]
        public async Task<ActionResult<List<BookingModel>>> GetBookingsByUserId(Guid userId)
        {
            var bookings = await _bookingsService.GetBookingsByUserIdAsync(userId);
            return Ok(bookings);
        }

        [HttpGet("roomBookings/{roomId}")]
        public async Task<ActionResult<List<BookingModel>>> GetBookingsByRoomId(Guid roomId)
        {
            var bookings = await _bookingsService.GetBookingsByRoomIdAsync(roomId);
            return Ok(bookings);
        }

        [HttpGet("bookingsStatistics")]
        public async Task<ActionResult<List<BookingsStatisticsDTO>>> GetBookingsStatistics()
        {
            var statistics = await _bookingsService.GetBookingsStatisticsAsync();
            return Ok(statistics);
        }

        [HttpPost("addBooking")]
        public async Task<ActionResult> AddBooking([FromBody] BookingCreateDTO bookingCreateDto)
        {
            await _bookingsService.AddBookingAsync(bookingCreateDto);
            return Ok();
        }

        [HttpPut("updateBooking/{bookingId}")]
        public async Task<ActionResult> UpdateBooking(Guid bookingId, [FromBody] BookingUpdateDTO bookingUpdateDto)
        {
            try
            {
                await _bookingsService.UpdateBookingAsync(bookingId, bookingUpdateDto);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("deleteBooking/{bookingId}")]
        public async Task<ActionResult> DeleteBooking(Guid bookingId)
        {
            try
            {
                await _bookingsService.DeleteBookingAsync(bookingId);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
