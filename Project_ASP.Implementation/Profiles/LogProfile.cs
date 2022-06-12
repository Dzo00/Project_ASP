using AutoMapper;
using Project_ASP.Application.BusinessLogic.DTO;
using Project_ASP.Application.Logger;
using Project_ASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.Profiles
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<Log, UseCaseLog>().ReverseMap();
            CreateMap<Log, LogDto>()
                .ForMember(x => x.EntityStatus, opt => opt.Ignore());
            CreateMap<LogDto, Log>();
        }
    }
}
