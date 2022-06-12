using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Project_ASP.Domain.Entities;
using Project_ASP.Domain.Enums;
using Project_ASP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.DataAccess
{
    public class ProjectContext : DbContext
    {

        public IApplicationUser User { get; set; }

        // -- Zakomentarisati ovaj konstruktor kada se radi update-database
        public ProjectContext(IApplicationUser user)
        {
            User = user;
        }
        // --
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=ASP_JL;Integrated Security=True");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.EntityStatus = eEntityStatus.Active;
                            e.CreatedAt = DateTime.UtcNow;
                            e.CreatedBy = User?.Identity;
                            e.ModifiedAt = null;
                            e.ModifiedBy = null;
                            break;
                        case EntityState.Modified:
                            e.ModifiedAt = DateTime.UtcNow;
                            e.ModifiedBy = User?.Identity;
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }

        #region DbSets
            public DbSet<Permission> Permissions { get; set; }
            public DbSet<Role> Roles { get; set; }
            public DbSet<User> Users { get; set; }
            public DbSet<Image> Images { get; set; }
            public DbSet<Diet> Diets { get; set; }
            public DbSet<Recipe> Recipes { get; set; }
            public DbSet<IngredientType> IngredientTypes { get; set; }
            public DbSet<Ingredient> Ingredients { get; set; }
            public DbSet<IngredientRecipe> IngredientRecipes { get; set; }
            public DbSet<RecipeImage> RecipeImages { get; set; }
            public DbSet<RolePermission> RolePermissions { get; set; }
            public DbSet<Rate> Rates { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<Log> Logs { get; set; }
        #endregion
    }
}
