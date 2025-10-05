using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Contracts.Rooms
{
    public class RoomUpdateDTO
    {
        public string? RoomName { get; set; }
        public int? RoomSize { get; set; }
        public decimal? PricePerNight { get; set; }
    }
}
