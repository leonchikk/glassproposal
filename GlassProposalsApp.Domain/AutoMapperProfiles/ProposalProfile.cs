using AutoMapper;
using GlassProposalsApp.Data.Models;
using GlassProposalsApp.Data.ReponseModels.Proposals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Domain.AutoMapperProfiles
{
    public class ProposalProfile : Profile
    {
        public ProposalProfile()
        {
            CreateMap<Proposals, ProposalResponseModel>()
                .ForMember(m => m.Initiator, opt => opt.MapFrom(p => p.Initiator))
                .ForMember(m => m.ProposalId, opt => opt.MapFrom(p => p.Id))
                .ForMember(m => m.Title, opt => opt.MapFrom(p => p.Title))
                .ForMember(m => m.RejectReason, opt => opt.MapFrom(p => p.RejectReason))
                .ForMember(m => m.IsPublic, opt => opt.MapFrom(p => !p.Process.IsPrivate))
                .ForMember(m => m.VacationData, opt => opt.MapFrom(p => new VacationData
                {
                    EndDate = p.Vacation.EndDate,
                    StartDate = p.Vacation.StartDate,
                    Reason = p.Vacation.Reason
                }));
        }
    }
}
