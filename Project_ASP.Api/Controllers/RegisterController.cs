using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_ASP.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly BaseHandler _handler;
        private readonly IRegisterUserCommand _command;

        public RegisterController(
            BaseHandler handler,
            IRegisterUserCommand command)
        {
            _handler = handler;
            _command = command;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] RegisterUserDto dto)
        {
                _handler.HandleCommand(_command, dto);
                return StatusCode(StatusCodes.Status201Created);
        }
    }
}
