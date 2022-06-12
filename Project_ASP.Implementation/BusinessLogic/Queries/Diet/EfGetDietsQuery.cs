using AutoMapper;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Application.DTO.Search;
using Project_ASP.Application.DTO.Wrapper;
using Project_ASP.Application.Extensions;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Entities;
using System.Linq;

namespace Project_ASP.Implementation.BusinessLogic
{
    public class EfGetDietsQuery : IGetDietsQuery
    {
        public int Id => 3;

        public string Name => "Get Diets";

        public string Description => "Display all diet details";

        private ProjectContext context { get; }
        private readonly IMapper mapper;

        public EfGetDietsQuery(ProjectContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public PagingResult<DietDto> Execute(PagedSearch search)
        {
            var query = context.Diets.Where(x => x.EntityStatus == Domain.Enums.eEntityStatus.Active);


            if (!string.IsNullOrEmpty(search.Keyword)) {
                var keyword = search.Keyword.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(keyword));
            }
            
            return query.PagedSearch<DietDto,Diet>(search, mapper);
        }
    }
}
