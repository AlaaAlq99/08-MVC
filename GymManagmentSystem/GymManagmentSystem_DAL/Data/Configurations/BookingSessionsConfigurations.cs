

using GymManagmentSystem_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagmentSystem_DAL.Data.Configurations
{
    public class BookingSessionsConfigurations: IEntityTypeConfiguration<BookingSessions>
    {
       public void Configure(EntityTypeBuilder<BookingSessions> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey(x => new { x.MemberId, x.SessionId });
            builder.HasOne(x => x.Member)
                   .WithMany(x => x.BookingSessions)
                   .HasForeignKey(x => x.MemberId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Session)
                   .WithMany(x => x.BookingSessions)
                     .HasForeignKey(x => x.SessionId)
                     .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CreatedAt).HasColumnName("BookingDate").HasDefaultValueSql("GetDate()");
        }
    }
}
