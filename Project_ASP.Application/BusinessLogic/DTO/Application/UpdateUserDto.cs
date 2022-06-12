using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.BusinessLogic.DTO.Application
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
    }
}
