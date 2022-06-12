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
    public class IngredientController : ControllerBase
    {
        private BaseHandler _handler;
        public IngredientController(BaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PagedSearch search, [FromServices] IGetIngredientsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id, [FromServices] IGetIngredientQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateIngredientDto dto, [FromServices] ICreateIngredientCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateIngredientDto dto, [FromServices] IUpdateIngredientCommand command)
        {
            dto.Id = id;
            _handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteIngredientCommand command)
        {
            _handler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}