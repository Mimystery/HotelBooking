using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<BookingEntity>
    {
        public void Configure(EntityTypeBuilder<BookingEntity> builder)
        {
            builder.HasKey(b => b.BookingId);

            builder.HasOne(b => b.User).WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);
            builder.HasOne(b => b.Room).WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId);
        }
    }
}
