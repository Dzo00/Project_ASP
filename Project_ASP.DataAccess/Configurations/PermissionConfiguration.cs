using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_ASP.Domain.Entities;

namespace Project_ASP.DataAccess.Configurations
{
    public class PermissionConfiguration : EntityConfiguration<Permission>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired(false);
            builder.HasMany(x=>x.Roles)
                   .WithOne(x=>x.Permission)
                   .HasForeignKey(x=>x.PermissionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
