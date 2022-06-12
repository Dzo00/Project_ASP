using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_ASP.Domain.Entities;
using System;

namespace Project_ASP.DataAccess.Configurations
{
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.EntityStatus).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.ModifiedAt).IsRequired(false);
            builder.Property(x => x.ModifiedBy).IsRequired(false);
            ConfigureRules(builder);
        }
        protected abstract void ConfigureRules(EntityTypeBuilder<T> builder);
    }
}
