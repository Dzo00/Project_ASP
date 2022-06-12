using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Application.DTO.Search;
using Project_ASP.Application.DTO.Wrapper;
using Project_ASP.Application.Extensions;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.BusinessLogic
{
    public class EfGetUsersQuery : IGetUsersQuery
    {
        private readonly ProjectContext context;
        private readonly IMapper mapper;

        public int Id => 23;

        public string Name => "Get Users";

        public string Description => "Display all users details";
        public EfGetUsersQuery(ProjectContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public PagingResult<UserDetailsDto> Execute(PagedSearch search)
        {
            var query = context.Users
                               .Include(x => x.Comments)
                               .Include(x => x.Rates)
                               .Include(x=>x.Recipes)
                               .Where(x => x.EntityStatus == Domain.Enums.eEntityStatus.Active);


            if (!string.IsNullOrEmpty(search.Keyword))
            {
                var keyword = search.Keyword.ToLower();
                query = query.Where(x => x.FirstName.ToLower().Contains(keyword)
                                      || x.DisplayName.ToLower().Contains(keyword)
                                      || x.LastName.ToLower().Contains(keyword)
                                      || x.Email.ToLower().Contains(keyword));
            }

            var result = query.PagedSearch<UserDetailsDto, User>(search, mapper);
            return result;
        }
    }
}
