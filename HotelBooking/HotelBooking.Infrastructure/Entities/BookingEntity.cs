using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Entities
{
    public class BookingEntity
    {
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public Guid RoomId { get; set; }
        public RoomEntity Room { get; set; }
        public Guid HotelId { get; set; }
        public HotelEntity Hotel { get; set; }
        public string HotelName { get; set; }
        public string City { get; set; }
        public string RoomName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
