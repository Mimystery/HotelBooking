using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Contracts.Bookings
{
    public class BookingCreateDTO
    {
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public Guid HotelId { get; set; }
        public string HotelName { get; set; }
        public string City { get; set; }
        public string RoomName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
