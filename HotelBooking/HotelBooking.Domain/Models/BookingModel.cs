using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models
{
    public class BookingModel
    {
        public BookingModel(Guid bookingId, Guid userId, Guid roomId, Guid hotelId, string hotelName, string city, string roomName, string firstName, string lastName, DateTime checkInDate, DateTime checkOutDate, decimal totalPrice, DateTime createdAt)
        {
            BookingId = bookingId;
            UserId = userId;
            RoomId = roomId;
            HotelId = hotelId;
            HotelName = hotelName;
            City = city;
            RoomName = roomName;
            FirstName = firstName;
            LastName = lastName;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            TotalPrice = totalPrice;
            CreatedAt = createdAt;
        }

        public Guid BookingId { get; }
        public Guid UserId { get; }
        public Guid RoomId { get; }
        public Guid HotelId { get; }
        public string HotelName { get; }
        public string City { get; }
        public string RoomName { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime CheckInDate { get; }
        public DateTime CheckOutDate { get; }
        public decimal TotalPrice { get; }
        public DateTime CreatedAt { get; }

    }
}
