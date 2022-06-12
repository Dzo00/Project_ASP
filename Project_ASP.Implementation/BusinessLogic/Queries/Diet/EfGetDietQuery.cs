using AutoMapper;
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
    public class EfGetDietQuery : IGetDietQuery
    {
        private readonly ProjectContext context;
        private readonly IMapper mapper;

        public int Id => 3;

        public string Name => "Get Diet Details";

        public string Description => "Get details from one diet";

        public EfGetDietQuery(ProjectContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public DietDto Execute(int id)
        {
            var diet = context.Diets.Find(id);
            if(diet == null || diet.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(Diet),id);
            }

            return mapper.Map<DietDto>(diet); 
        }
    }
}
