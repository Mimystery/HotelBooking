using AutoMapper;
using HotelBooking.Application.Services;
using HotelBooking.Domain.Contracts.Hotels;
using HotelBooking.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelsService _hotelsService;
        private readonly IMapper _mapper;

        public HotelsController(IHotelsService hotelsService, IMapper mapper)
        {
            _hotelsService = hotelsService;
            _mapper = mapper;
        }

        [HttpGet("allHotels")]
        public async Task<ActionResult<List<HotelModel>>> GetAllHotels()
        {
            var hotels = await _hotelsService.GetAllHotelsAsync();
            return Ok(hotels);
        }

        [HttpGet("hotelById/{hotelId}")]
        public async Task<ActionResult<HotelModel>> GetHotelById(Guid hotelId)
        {
            try
            {
                var hotel = await _hotelsService.GetHotelByIdAsync(hotelId);
                return Ok(hotel);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("addHotel")]
        public async Task<ActionResult<Guid>> AddHotel([FromBody] HotelCreateDTO hotelCreateDto)
        {
            var hotelId = await _hotelsService.AddHotelAsync(hotelCreateDto);
            return Ok(new { hotelId });
        }

        [HttpPut("updateHotel/{hotelId}")]
        public async Task<ActionResult> UpdateHotel(Guid hotelId, [FromBody] HotelUpdateDTO hotelUpdateDto)
        {
            try
            {
                await _hotelsService.UpdateHotelAsync(hotelId, hotelUpdateDto);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("deleteHotel/{hotelId}")]
        public async Task<ActionResult> DeleteHotel(Guid hotelId)
        {
            try
            {
                await _hotelsService.DeleteHotelAsync(hotelId);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }
}
