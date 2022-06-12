using AutoMapper;
using Project_ASP.Application.BusinessLogic.DTO;
using Project_ASP.Application.DTO.Search;
using Project_ASP.Application.DTO.Wrapper;
using Project_ASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.Extensions
{
    public static class Extensions 
    { 
        public static PagingResult<T> PagedSearch<T, TEntity>(this IQueryable<TEntity> table, PagedSearch search, IMapper mapper)
            where T : BaseDto
        {
            var skipCount = search.PerPage.Value * (search.Page.Value - 1);

            var skipped = table.Skip(skipCount).Take(search.PerPage.Value);

            var response = new PagingResult<T>
            {
                Page = search.Page.Value,
                PerPage = search.PerPage.Value,
                TotalCount = table.Count(),
                Data = mapper.Map<IEnumerable<T>>(skipped)
            };

            return response;
        }
    }
}
