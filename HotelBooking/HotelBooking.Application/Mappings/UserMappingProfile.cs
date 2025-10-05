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
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserModel, UserEntity>();
            CreateMap<UserEntity, UserModel>();
        }
    }
}
