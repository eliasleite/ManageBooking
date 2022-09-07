using Booking.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Data.Mappings
{
    public class RoomMapping : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Floor)
               .IsRequired();

            builder.Property(r => r.Number)
              .IsRequired();

            builder.Property(r => r.Price)
              .IsRequired();

            builder.HasMany(r => r.Reservations)
                .WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId);

            builder.ToTable("Rooms");
        }
    }
}
