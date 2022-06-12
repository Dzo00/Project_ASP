using AutoMapper;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentDto, Comment>().ReverseMap();
        }
    }
}
