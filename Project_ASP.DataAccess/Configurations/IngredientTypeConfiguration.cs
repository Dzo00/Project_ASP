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
    public class IngredientTypeConfiguration : EntityConfiguration<IngredientType>
    {
        protected override void ConfigureRules(EntityTypeBuilder<IngredientType> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(35);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasMany(x => x.Ingredients)
                   .WithOne(x => x.IngredientType)
                   .HasForeignKey(x => x.IngredientTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
