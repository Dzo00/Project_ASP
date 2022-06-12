using Project_ASP.Application.BusinessLogic.DTO;
using Project_ASP.Application.DTO.Search;
using Project_ASP.Application.DTO.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.BusinessLogic
{
    public interface ISearchLogsQuery : IBaseQuery<LogSearch,PagingResult<LogDto>>
    {
    }
}
