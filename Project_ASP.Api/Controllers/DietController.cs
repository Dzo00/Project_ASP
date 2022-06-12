using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO;
using Project_ASP.Application.DTO.Search;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_ASP.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class DietController : ControllerBase
    {
        private BaseHandler _handler;
        public DietController(BaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PagedSearch search, [FromServices] IGetDietsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id, [FromServices] IGetDietQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] ILookupDto dto, [FromServices] ICreateDietCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] ILookupDto dto, [FromServices] IUpdateDietCommand command)
        {
            dto.Id = id;
            _handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteDietCommand command)
        {
            _handler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
