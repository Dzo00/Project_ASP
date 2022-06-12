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
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.ActionName).IsRequired();
            builder.Property(x => x.IsAuthorized).IsRequired();
            builder.Property(x => x.ExecutedOn).IsRequired();

            builder.HasIndex(x => x.Username);
            builder.HasIndex(x => x.ActionName);
        }
    }
}
