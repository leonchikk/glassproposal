using AutoMapper;
using GlassProposalsApp.Data;
using GlassProposalsApp.Data.Interfaces;
using GlassProposalsApp.Data.Models;
using GlassProposalsApp.Data.ReponseModels;
using GlassProposalsApp.Data.Repositories;
using GlassProposalsApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Domain.Services
{
    public class DecisionService : IDecisionService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DecisionService(GlassProposalContext dbContext, UnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public IEnumerable<UserResponseModel> GetDecisionMakersForFirstStage(int processType)
        {
            return _mapper.Map<IEnumerable<UserResponseModel>>(_unitOfWork.Users.GetDecisionMakersForFirstStage(processType));
        }

        public IEnumerable<UserResponseModel> GetDecisionMakersForNextStage(Guid proposalId)
        {
            return _mapper.Map<IEnumerable<UserResponseModel>>(_unitOfWork.Users.GetDecisionMakersForNextStage(proposalId));
        }
    }
}
