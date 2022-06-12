using Project_ASP.Application.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.DTO.Wrapper
{
    public class PagingResult<T> where T : BaseDto
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int PagesCount => (int)Math.Ceiling((float)TotalCount / PerPage);
        public IEnumerable<T> Data { get; set; }
    }
}
