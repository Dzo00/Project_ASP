using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_ASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.DataAccess.Configurations
{
    public class RateConfiguration : EntityConfiguration<Rate>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Rate> builder)
        {
            builder.Property(x => x.RateValue).IsRequired();
            builder.HasIndex(x => new { x.RecipeId, x.UserId }).IsUnique();
        }
    }
}
