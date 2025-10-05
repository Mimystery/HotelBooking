using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Entities;

namespace HotelBooking.Application.Mappings
{
    public class RoomMappingProfile : Profile
    {
        public RoomMappingProfile()
        {
            CreateMap<RoomEntity, RoomModel>();
            CreateMap<RoomModel, RoomEntity>();
        }
    }
}
