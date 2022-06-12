using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_ASP.Domain.Entities;
using System;

namespace Project_ASP.DataAccess.Configurations
{
    public class CommentConfiguration : EntityConfiguration<Comment>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.RecipeId).IsRequired();
            builder.Property(x => x.CommentText).IsRequired();
        }
    }
}
