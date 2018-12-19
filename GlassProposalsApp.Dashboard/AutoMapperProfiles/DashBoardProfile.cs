using AutoMapper;
using GlassProposalsApp.Data.Entities;
using GlassProposalsApp.Data.ReponseModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Dashboard.AutoMapperProfiles
{
    public class DashBoardProfile : Profile
    {
        public DashBoardProfile()
        {
            CreateMap<Users, UserResponseModel>()
                .ForMember(x => x.UserTypes, opt => opt.MapFrom(x => x.UserTypes.Select(userType => userType.UserType)));
        }
    }
}
