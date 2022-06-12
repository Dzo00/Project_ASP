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
    public class IngredientRecipeConfiguration : EntityConfiguration<IngredientRecipe>
    {
        protected override void ConfigureRules(EntityTypeBuilder<IngredientRecipe> builder)
        {
            builder.Property(x => x.RecipeId).IsRequired();
            builder.Property(x => x.IngredientId).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.MeasureId).IsRequired();

            builder.HasIndex(x => new { x.RecipeId, x.IngredientId }).IsUnique();
        }
    }
}
