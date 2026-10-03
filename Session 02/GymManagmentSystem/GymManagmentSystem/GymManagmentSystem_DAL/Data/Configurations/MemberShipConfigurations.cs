using GymManagmentSystem_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_DAL.Data.Configurations
{
    internal class MemberShipConfigurations : IEntityTypeConfiguration<MemberShip>
    {
        public new void Configure(EntityTypeBuilder<MemberShip> builder)
        {
            builder.Ignore(x => x.Id);
            builder.Ignore(x => x.Status);
            builder.Ignore(x => x.IsActive);

            builder.Property(x => x.CreatedAt).HasColumnName("StartDate").HasDefaultValueSql("GetDate()");

            builder.HasKey(x => new { x.MemberId, x.PlanId });
        }
    
    }
}
