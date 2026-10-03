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
    public class MemberSessionConfigurations : IEntityTypeConfiguration<MemberSessions>
    {
        public new void Configure(EntityTypeBuilder<MemberSessions> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey(x => new { x.MemberId, x.SessionId });

        }
    }
}
