using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_ASP.Domain.Entities;

namespace Project_ASP.DataAccess.Configurations
{
    public class IngredientConfiguration : EntityConfiguration<Ingredient>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Ingredient> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.IngredientTypeId).IsRequired();
            builder.HasMany(x => x.Recipes)
                   .WithOne(x => x.Ingredient)
                   .HasForeignKey(x=>x.IngredientId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
