using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Contracts.Bookings
{
    public class BookingUpdateDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public decimal? TotalPrice { get; set; }

    }
}
