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
    public class HotelConfiguration : IEntityTypeConfiguration<HotelEntity>
    {
        public void Configure(EntityTypeBuilder<HotelEntity> builder)
        {
            builder.HasKey(h => h.HotelId);

            builder.HasMany(h => h.Rooms).WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
