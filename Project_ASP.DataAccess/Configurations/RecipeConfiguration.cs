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
    public class RecipeConfiguration : EntityConfiguration<Recipe>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Recipe> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Guide).IsRequired();
            builder.Property(x => x.AvgRate).IsRequired();
            builder.Property(x => x.NumOfServings).IsRequired();
            builder.Property(x => x.TimeToCook).IsRequired();
            builder.Property(x => x.DietId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();

            builder.HasIndex(x => x.Title);

            builder.HasMany(x => x.Images)
                   .WithOne(x => x.Recipe)
                   .HasForeignKey(x => x.RecipeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Ingredients)
                   .WithOne(x => x.Recipe)
                   .HasForeignKey(x => x.RecipeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Comments)
                   .WithOne(x => x.Recipe)
                   .HasForeignKey(x => x.RecipeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Rates)
                   .WithOne(x => x.Recipe)
                   .HasForeignKey(x => x.RecipeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
