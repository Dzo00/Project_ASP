using Microsoft.AspNetCore.Mvc;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_ASP.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InitialDataController : ControllerBase
    {
        private ProjectContext Context { get; set; }
        public InitialDataController(ProjectContext context)
        {
            Context = context;
        }

        // POST api/<InitialDataController>
        [HttpPost]
        public IActionResult Post()
        {
            var roles = new List<Role>
            {
                new Role
                {
                    Name = "User"
                },
                new Role
                {
                    Name = "Admin"
                }
            };


            // Password je Test1234
            var users = new List<User>
            {
                 new User{
                    FirstName = "Jovana",
                    LastName  = "User",
                    DisplayName = "Jovana User",
                    Role = roles.First(),
                    Email = "userjovana@gmail.com",
                    Password = "$2a$11$E.xIURNmYBZSBIxPcklpH.oWEAmQn98meJVxi9C6R.68WiF/MYiZ2"
                },
                 new User{
                    FirstName = "Jovana",
                    LastName  = "Admin",
                    DisplayName = "Jovana Admin",
                    Role = roles.Last(),
                    Email = "adminjovana@gmail.com",
                    Password = "$2a$11$E.xIURNmYBZSBIxPcklpH.oWEAmQn98meJVxi9C6R.68WiF/MYiZ2"
                }
            };

            var permissions = new List<Permission> {
                new Permission {
                    Name = "Register",
                    Description = "Creation of user account"
                },
                new Permission {
                    Name = "Create Diet",
                    Description = "Add new diet"
                },
                new Permission {
                    Name = "Get Diets",
                    Description = "Display all diet details"
                },
                new Permission {
                    Name = "Get Diet",
                    Description = "Display single diet details"
                },
                new Permission {
                    Name = "Update Diet",
                    Description = "Update data for diet"
                },
                new Permission {
                    Name = "Delete Diet",
                    Description = "Soft delete of diet in the system"
                },
                new Permission {
                    Name = "Create Ingredient Type",
                    Description = "Add new ingredient type"
                },
                new Permission {
                    Name = "Get Ingredient Types",
                    Description = "Display all ingredient type details"
                },
                new Permission {
                    Name = "Get Ingredient Type",
                    Description = "Display single ingredient type details"
                },
                new Permission {
                    Name = "Update Ingredient Type",
                    Description = "Update data for ingredient type"
                },
                new Permission {
                    Name = "Delete Ingredient Type",
                    Description = "Soft delete of ingredient type in the system"
                },
                new Permission {
                    Name = "Create Ingredient",
                    Description = "Add new ingredient"
                },
                new Permission {
                    Name = "Get Ingredients",
                    Description = "Display all ingredient details"
                },
                new Permission {
                    Name = "Get Ingredient",
                    Description = "Display single ingredient details"
                },
                new Permission {
                    Name = "Update Ingredient",
                    Description = "Update data for ingredient"
                },
                new Permission {
                    Name = "Delete Ingredient",
                    Description = "Soft delete of ingredient in the system"
                },
                new Permission {
                    Name = "Create Recipe",
                    Description = "Add new recipe"
                },
                new Permission {
                    Name = "Get Recipes",
                    Description = "Display all recipe details"
                },
                new Permission {
                    Name = "Get Recipe",
                    Description = "Display single recipe details"
                },
                new Permission {
                    Name = "Update Recipe",
                    Description = "Update data for recipe"
                },
                new Permission {
                    Name = "Delete Recipe",
                    Description = "Soft delete of recipe in the system"
                },
                new Permission {
                    Name = "Create New Admin",
                    Description = "Add new admin"
                },
                new Permission {
                    Name = "Get Users",
                    Description = "Display all users details"
                },
                new Permission {
                    Name = "Get User",
                    Description = "Display single user details"
                },
                new Permission {
                    Name = "Update Account",
                    Description = "Update account detail"
                },
                new Permission {
                    Name = "Delete Account",
                    Description = "Delete account"
                },
                new Permission {//26
                    Name = "Rate Recipe",
                    Description = "Add rate to a recipe"
                },
                new Permission {
                    Name = "Change Rate",
                    Description = "Update your rate"
                },
                new Permission {
                    Name = "Add Comment on recipe",
                    Description = "Add comment to recipe"
                },
                new Permission {
                    Name = "Delete Comment",
                    Description = "Delete Comment from recipe"
                }
            };

            var rolePermissions = new List<RolePermission> {
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(1)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(2)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(3)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(4)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(5)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(6)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(7)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(8)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(9)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(10)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(11)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(12)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(13)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(14)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(15)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(16)
                },
                new RolePermission
                {
                    Role = roles.First(),
                    Permission = permissions.ElementAt(16)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(17)
                },
                new RolePermission
                {
                    Role = roles.First(),
                    Permission = permissions.ElementAt(17)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(18)
                },
                new RolePermission
                {
                    Role = roles.First(),
                    Permission = permissions.ElementAt(18)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(19)
                },
                new RolePermission
                {
                    Role = roles.First(),
                    Permission = permissions.ElementAt(19)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(20)
                },
                new RolePermission
                {
                    Role = roles.First(),
                    Permission = permissions.ElementAt(20)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(21)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(22)
                },
                new RolePermission
                {
                    Role = roles.First(),
                    Permission = permissions.ElementAt(23)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(23)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(24)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(25)
                },
                new RolePermission
                {
                    Role = roles.First(),
                    Permission = permissions.ElementAt(24)
                },
                new RolePermission
                {
                    Role = roles.First(),
                    Permission = permissions.ElementAt(25)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(26)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(27)
                },
                new RolePermission
                {
                    Role = roles.First(),
                    Permission = permissions.ElementAt(26)
                },
                new RolePermission
                {
                    Role = roles.First(),
                    Permission = permissions.ElementAt(27)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(28)
                },
                new RolePermission
                {
                    Role = roles.Last(),
                    Permission = permissions.ElementAt(29)
                },
                new RolePermission
                {
                    Role = roles.First(),
                    Permission = permissions.ElementAt(28)
                },
                new RolePermission
                {
                    Role = roles.First(),
                    Permission = permissions.ElementAt(29)
                },
            };

            var diets = new List<Diet>
            {
                new Diet
                {
                    Name = "Keto"
                },
                new Diet
                {
                    Name = "Vegan"
                },
                new Diet
                {
                    Name = "Low Carb"
                },
                new Diet
                {
                    Name = "Gluten-Free"
                },
                new Diet
                {
                    Name = "Dairy-Free"
                },
                new Diet
                {
                    Name = "Vegetarian"
                },
                new Diet
                {
                    Name = "Normal"
                }
            };

            var ingredientTypes = new List<IngredientType> {
                new IngredientType {
                    Name = "Dairy"
                },
                new IngredientType {
                    Name = "Vegetables"
                },
                new IngredientType {
                    Name = "Fruits"
                },
                new IngredientType {
                    Name = "Meat, Sausages and Fish"
                },
                new IngredientType {
                    Name = "Herbs and Spices"
                },
                new IngredientType {
                    Name = "Fats and Oils"
                },
                new IngredientType {
                    Name = "Pasta, rice and pulses"
                },
                new IngredientType
                {
                    Name = "Grain, nuts and baking products"
                }
            };

            var ingredients = new List<Ingredient> {
                new Ingredient { Name = "Eggs", IngredientType = ingredientTypes.First()},
                new Ingredient { Name = "Milk", IngredientType = ingredientTypes.First()},
                new Ingredient { Name = "Goat cheese", IngredientType = ingredientTypes.First()},
                new Ingredient { Name = "Carrot", IngredientType = ingredientTypes.ElementAt(1)},
                new Ingredient { Name = "Onion", IngredientType = ingredientTypes.ElementAt(1)},
                new Ingredient { Name = "Red Bell Pepper", IngredientType = ingredientTypes.ElementAt(1)},
                new Ingredient { Name = "Apple", IngredientType = ingredientTypes.ElementAt(2)},
                new Ingredient { Name = "Blueberry", IngredientType = ingredientTypes.ElementAt(2)},
                new Ingredient { Name = "Mango", IngredientType = ingredientTypes.ElementAt(2)},
                new Ingredient { Name = "Chicken Breasts", IngredientType = ingredientTypes.ElementAt(3)},
                new Ingredient { Name = "Salmon", IngredientType = ingredientTypes.ElementAt(3)},
                new Ingredient { Name = "Rosemary", IngredientType = ingredientTypes.ElementAt(4)},
                new Ingredient { Name = "Salt", IngredientType = ingredientTypes.ElementAt(4)},
                new Ingredient { Name = "Pepper", IngredientType = ingredientTypes.ElementAt(4)},
                new Ingredient { Name = "Cocount Oil", IngredientType = ingredientTypes.ElementAt(5)},
                new Ingredient { Name = "Dry Beans", IngredientType = ingredientTypes.ElementAt(5)},
                new Ingredient { Name = "Peas", IngredientType = ingredientTypes.ElementAt(6)},
                new Ingredient { Name = "Sunflower Oil", IngredientType = ingredientTypes.ElementAt(6)},
                new Ingredient { Name = "Vanilla extract", IngredientType = ingredientTypes.ElementAt(7)},
                new Ingredient { Name = "Flour", IngredientType = ingredientTypes.ElementAt(7)},
            };

            var pictures = new List<Image>
            {
                new Image {Path = "1379cbbd-3c90-4a19-a346-27e466f959a3_spellbreak.png", Alt = "spellbreak"},
                new Image {Path = "464cb3d9-3239-4ba1-b643-8359d4af6087_as-cr-odys.jpg", Alt = "odys"},
                new Image {Path = "97a46c18-8118-453d-8e57-b165c0dbd8ed_as-cr-rogue.jpg", Alt = "rogue"},
            };

            var recipes = new List<Recipe>
            {
                new Recipe {
                    Title = "Prvi recept",
                    Description = "Prvi recept neki opis",
                    Guide = "Prvi recept guide za njega",
                    TimeToCook = 50,
                    NumOfServings = 4,
                    Diet = diets.First(),
                    AvgRate = 0,
                    User = users.Last()
                },
                new Recipe {
                    Title = "Drigi recept",
                    Description = "Drigi recept neki opis",
                    Guide = "Drigi recept guide za njega",
                    TimeToCook = 150,
                    NumOfServings = 6,
                    Diet = diets.Last(),
                    AvgRate = 0,
                    User = users.First()
                }
            };

            var recipeImages = new List<RecipeImage> {
                new RecipeImage { Recipe = recipes.First(), Image = pictures.First() },
                new RecipeImage { Recipe = recipes.First(), Image = pictures.ElementAt(1) },
                new RecipeImage { Recipe = recipes.Last(), Image = pictures.Last() }
            };

            var comments = new List<Comment> {
                new Comment { Recipe = recipes.First(), User = users.First(), CommentText = "Odlican recept, za svaku preporuku" },
                new Comment { Recipe = recipes.First(), User = users.Last(), CommentText = "Hvala :)" }, // cringe x 2
                new Comment { Recipe = recipes.Last(), User = users.Last(), CommentText = "Ispalo mi je kao sa slike, zadovolja sam. BRAVO!" } // cringe
            };

            var ingredientRecipes = new List<IngredientRecipe> {
                new IngredientRecipe{ Recipe=recipes.First(), Ingredient = ingredients.First(), Measure = Domain.Enums.eMeasure.NoMeasure, Quantity = "3" },
                new IngredientRecipe{ Recipe=recipes.First(), Ingredient = ingredients.ElementAt(1), Measure = Domain.Enums.eMeasure.ml, Quantity = "250" },
                new IngredientRecipe{ Recipe=recipes.First(), Ingredient = ingredients.ElementAt(19), Measure = Domain.Enums.eMeasure.gr, Quantity = "250" },
                new IngredientRecipe{ Recipe=recipes.First(), Ingredient = ingredients.ElementAt(18), Measure = Domain.Enums.eMeasure.packet, Quantity = "1" },
                new IngredientRecipe{ Recipe=recipes.Last(), Ingredient = ingredients.First(), Measure = Domain.Enums.eMeasure.NoMeasure, Quantity = "4" },
                new IngredientRecipe{ Recipe=recipes.Last(), Ingredient = ingredients.ElementAt(9), Measure = Domain.Enums.eMeasure.gr, Quantity = "450" },
                new IngredientRecipe{ Recipe=recipes.Last(), Ingredient = ingredients.ElementAt(19), Measure = Domain.Enums.eMeasure.gr, Quantity = "250" },
                new IngredientRecipe{ Recipe=recipes.Last(), Ingredient = ingredients.ElementAt(12), Measure = Domain.Enums.eMeasure.NoMeasure, Quantity = "po ukusu" },
                new IngredientRecipe{ Recipe=recipes.Last(), Ingredient = ingredients.ElementAt(13), Measure = Domain.Enums.eMeasure.NoMeasure, Quantity = "po ukusu" },
            };

            // Moram ovako za permisije, jer mi se poremeti redosled ako je AddRange
            foreach (var per in permissions)
            {
                Context.Permissions.Add(per);
                Context.SaveChanges();
            }

            Context.Roles.AddRange(roles);
            Context.Users.AddRange(users);
            Context.RolePermissions.AddRange(rolePermissions);
            Context.Diets.AddRange(diets);
            Context.IngredientTypes.AddRange(ingredientTypes);
            Context.Ingredients.AddRange(ingredients);
            Context.Images.AddRange(pictures);
            Context.Recipes.AddRange(recipes);
            Context.RecipeImages.AddRange(recipeImages);
            Context.Comments.AddRange(comments);
            Context.IngredientRecipes.AddRange(ingredientRecipes);
            Context.SaveChanges();
            return StatusCode(201);
        }
    }
}
