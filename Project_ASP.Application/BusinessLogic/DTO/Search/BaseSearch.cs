using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.DTO.Search
{
    public class BaseSearch
    {
        public string Keyword { get; set; }
    }

    public class PagedSearch : BaseSearch{
        public int? Page { get; set; } = 1;
        public int? PerPage { get; set; } = 10;
    }

    public class LogSearch : PagedSearch
    {
        public int UserId { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime DateFrom { get; set; }
    }
}
