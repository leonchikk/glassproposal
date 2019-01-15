using AutoMapper;
using GlassProposalsApp.Data.Entities;
using GlassProposalsApp.Data.Models.Users;
using GlassProposalsApp.Data.ReponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Application.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Users, ProfileResponseModel>()
                .ForMember(m => m.User, opt => opt.MapFrom(x => new UserResponseModel { Id = x.Id, Name = x.Name }))
                .ForMember(m => m.VacationDaysLeft, opt => opt.MapFrom(x => x.VacationDaysLeft - x.Vacations.Where(vacation => vacation.StartDate.Year == DateTime.Now.Year
                                                                                                          && vacation.IsApproved).Sum(vacation => vacation.Duration)))

                .ForMember(m => m.BonusBalance, opt => opt.MapFrom(x => x.BonusBalance - x.Bonuses.Where(bonus => bonus.Date.Year == DateTime.Now.Year ).Sum(bonus => bonus.Spent)))
                .ForMember(m => m.UserLevel, opt => opt.MapFrom(x => x.UserLevel))
                .ForMember(m => m.Mentor, opt => opt.MapFrom(x => x.Mentor));
        }
    }
}
