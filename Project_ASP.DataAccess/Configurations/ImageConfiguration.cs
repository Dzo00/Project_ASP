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
    public class ImageConfiguration : EntityConfiguration<Image>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Image> builder)
        {
            builder.Property(x => x.Path).IsRequired();
            builder.Property(x => x.Alt).IsRequired().HasMaxLength(30);

            builder.HasMany(x => x.Recipes)
                   .WithOne(x => x.Image)
                   .HasForeignKey(x => x.ImageId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
