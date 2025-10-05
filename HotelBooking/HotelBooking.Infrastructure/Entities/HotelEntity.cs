using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Entities
{
    public class HotelEntity
    {
        public Guid HotelId { get; set; }
        public string HotelName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public List<RoomEntity> Rooms { get; set; }
    }
}
