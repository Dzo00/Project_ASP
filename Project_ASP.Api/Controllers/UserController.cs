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
    public class UserController : ControllerBase
    {
        private BaseHandler _handler;
        public UserController(BaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PagedSearch search, [FromServices] IGetUsersQuery query)
        {
            return Ok(_handler.HandleQuery(query, search)); ;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetUserQuery query)
        {
            return Ok(_handler.HandleQuery(query, id)); ;
        }

        [HttpPost] // Registracija novog admina
        public IActionResult Post([FromBody] RegisterUserDto dto, [FromServices] IAddNewAdminCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        [HttpPost("CommentRecipe")]
        public IActionResult CommentRecipe([FromBody] CreateCommentDto dto, [FromServices] IAddCommentCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        [HttpPost("RateRecipe")]
        public IActionResult RateRecipe([FromBody] CreateRateDto dto, [FromServices] IRateRecipeCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        [HttpDelete("DeleteComment/{id}")]
        public IActionResult DeleteComment(int id, [FromServices] IDeleteCommentCommand command)
        {
            _handler.HandleCommand(command, id);
            return StatusCode(204);
        }

        [HttpPut("UpdateRate/{id}")]
        public IActionResult UpdateRate(int id, [FromBody] UpdateRateDto dto, [FromServices] IUpdateRateCommand command)
        {
            dto.Id = id;
            _handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        [HttpPut("{id}")] // Update User Details
        public IActionResult Put(int id, [FromBody] UpdateUserDto dto, [FromServices] IUpdateUserCommand command)
        {
            dto.Id = id;
            _handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        [HttpDelete("{id}")] // Delete Account, samo svoj moze
        public IActionResult Delete(int id, [FromServices] IDeleteAccountCommand command)
        {
            _handler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
