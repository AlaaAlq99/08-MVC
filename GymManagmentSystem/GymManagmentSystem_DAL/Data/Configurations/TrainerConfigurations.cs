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
    public class TrainerConfigurations: GymUserConfigurations<Trainer>
    {
    
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(x => x.CreatedAt).HasColumnName("Hiring Date").HasDefaultValueSql("GetDate()");

        }
        
    }
}
