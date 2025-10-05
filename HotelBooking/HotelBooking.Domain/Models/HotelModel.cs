using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models
{
    public class HotelModel
    {
        public HotelModel(Guid hotelId, string hotelName, string address, string city, string description, IReadOnlyList<RoomModel> rooms)
        {
            HotelId = hotelId;
            HotelName = hotelName;
            Address = address;
            City = city;
            Description = description;
            Rooms = rooms;
        }

        public Guid HotelId { get; }
        public string HotelName { get; }
        public string Address { get; }
        public string City { get; }
        public string Description { get; }
        public IReadOnlyList<RoomModel> Rooms { get; }
    }
}
