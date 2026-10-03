

using GymManagmentSystem_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace GymManagmentSystem_DAL.Data.Configurations
{
    public class PlansConfigurations: IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(200);
            builder.Property(x => x.Price).HasPrecision(10, 2);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GetDate()");
            builder.ToTable(tb => tb.HasCheckConstraint("PlanDurationCheck", "DurationDays Between 1 and 365"));
        }
    }
}
