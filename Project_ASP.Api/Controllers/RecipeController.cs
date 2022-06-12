using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Application.DTO.Search;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_ASP.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class RecipeController : ControllerBase
    {
        private BaseHandler _handler;
        public RecipeController(BaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery] PagedSearch search, [FromServices] IGetRecipesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search)); ;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id, [FromServices] IGetRecipeQuery query)
        {
            return Ok(_handler.HandleQuery(query, id)); ;
        }

        [HttpPost]
        public IActionResult Post([FromForm] CreateRecipeDto dto,[FromServices] ICreateRecipeCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] UpdateRecipeDto dto, [FromServices] IUpdateRecipeCommand command)
        {
            dto.Id = id;
            _handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteRecipeCommand command)
        {
            _handler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
