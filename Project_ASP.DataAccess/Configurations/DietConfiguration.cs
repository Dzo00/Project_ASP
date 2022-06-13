using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_ASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.DataAccess.Configurations
{
    public class DietConfiguration : EntityConfiguration<Diet>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Diet> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasMany(x => x.Recipes)
                    .WithOne(x => x.Diet)
                    .HasForeignKey(x => x.DietId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
