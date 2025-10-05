using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Entities
{
    public class RoomEntity
    {
        public Guid RoomId { get; set; }
        public Guid HotelId { get; set; }
        public HotelEntity Hotel { get; set; }
        public string RoomName { get; set; }
        public int RoomSize { get; set; }
        public decimal PricePerNight { get; set; }
        public List<BookingEntity> Bookings { get; set; }
    }
}
