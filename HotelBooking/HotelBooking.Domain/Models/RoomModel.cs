using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models
{
    public class RoomModel
    {
        public RoomModel(Guid roomId, Guid hotelId, string roomName, int roomSize, decimal pricePerNight)
        {
            RoomId = roomId;
            HotelId = hotelId;
            RoomName = roomName;
            RoomSize = roomSize;
            PricePerNight = pricePerNight;
        }
        public Guid RoomId { get; }
        public Guid HotelId { get; }
        public string RoomName { get; }
        public int RoomSize { get; }
        public decimal PricePerNight { get; }
    }
}
