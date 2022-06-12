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

        //[HttpPost]
        //public IActionResult CommentPost([FromBody] RegisterUserDto dto, [FromServices] IAddNewAdminCommand command)
        //{
        //    _handler.HandleCommand(command, dto);
        //    return StatusCode(204);
        //}

        //[HttpPost]
        //public IActionResult RatePost([FromBody] RegisterUserDto dto, [FromServices] IAddNewAdminCommand command)
        //{
        //    _handler.HandleCommand(command, dto);
        //    return StatusCode(204);
        //}

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateUserDto dto, [FromServices] IUpdateUserCommand command)
        {
            dto.Id = id;
            _handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteAccountCommand command)
        {
            _handler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
