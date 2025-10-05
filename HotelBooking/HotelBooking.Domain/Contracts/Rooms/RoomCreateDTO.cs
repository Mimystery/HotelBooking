using HotelBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Contracts.Rooms
{
    public class RoomCreateDTO
    {
        public Guid HotelId { get; set; }
        public string RoomName { get; set; }
        public int RoomSize { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
