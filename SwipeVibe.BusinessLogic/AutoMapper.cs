using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.BusinessLogic
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserReturn>()
                .ForMember(d => d.Role,
                           o => o.MapFrom(s => s.Role.ToString()));
        }
    }

    public static class MapperBootstrap
    {
        public static readonly IMapper Mapper =
            new MapperConfiguration(c => c.AddProfile<AutoMapperProfile>()).CreateMapper();
    }
}
