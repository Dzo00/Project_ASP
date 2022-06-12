using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Application.Exceptions;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.BusinessLogic
{ 
    public class EfGetUserQuery : IGetUserQuery
    {
        private readonly ProjectContext context;
        private readonly IMapper mapper;

        public int Id => 24;

        public string Name => "Get User Details";

        public string Description => "Get details from one user";

        public EfGetUserQuery(ProjectContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public UserDetailsDto Execute(int id)
        {
            var user = context.Users
                                .Include(x => x.Comments)
                                .Include(x => x.Rates)
                                .Include(x => x.Recipes)
                                .Where(x => x.Id == id).FirstOrDefault();

            if (user == null || user.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(User),id);
            }

            return mapper.Map<UserDetailsDto>(user); 
        }
    }
}
