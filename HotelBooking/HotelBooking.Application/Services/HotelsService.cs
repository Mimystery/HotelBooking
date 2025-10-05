using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelBooking.Domain.Contracts.Hotels;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Repositories;

namespace HotelBooking.Application.Services
{
    public class HotelsService : IHotelsService
    {
        private readonly IHotelsRepository _hotelsRepository;
        private readonly IMapper _mapper;

        public HotelsService(IHotelsRepository hotelsRepository, IMapper mapper)
        {
            _hotelsRepository = hotelsRepository;
            _mapper = mapper;
        }

        public async Task<List<HotelModel>> GetAllHotelsAsync()
        {
            return await _hotelsRepository.GetAllHotelsAsync();
        }

        public async Task<HotelModel> GetHotelByIdAsync(Guid hotelId)
        {
            return await _hotelsRepository.GetHotelByIdAsync(hotelId);
        }

        public async Task<Guid> AddHotelAsync(HotelCreateDTO hotelCreateDto)
        {
            var hotelModel = new HotelModel(
                Guid.NewGuid(),
                hotelCreateDto.HotelName,
                hotelCreateDto.Address,
                hotelCreateDto.City,
                hotelCreateDto.Description,
                new List<RoomModel>()
            );

            await _hotelsRepository.AddHotelAsync(hotelModel);
            return hotelModel.HotelId;
        }

        public async Task UpdateHotelAsync(Guid hotelId, HotelUpdateDTO hotelUpdateDto)
        {
            await _hotelsRepository.UpdateHotelAsync(hotelId, hotelUpdateDto);
        }

        public async Task DeleteHotelAsync(Guid hotelId)
        {
            await _hotelsRepository.DeleteHotelAsync(hotelId);
        }
    }
}
