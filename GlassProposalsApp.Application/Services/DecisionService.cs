using AutoMapper;
using GlassProposalsApp.Data;
using GlassProposalsApp.Data.Interfaces;
using GlassProposalsApp.Data.Entities;
using GlassProposalsApp.Data.ReponseModels;
using GlassProposalsApp.Data.Repositories;
using GlassProposalsApp.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Application.Services
{
    public class DecisionService : IDecisionService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DecisionService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public void ApproveProposal(Guid proposalId, Guid changeInitiatorId, Guid? nextDecisionMakerId)
        {
            _unitOfWork.Proposals.Approve(proposalId, changeInitiatorId, nextDecisionMakerId);
            _unitOfWork.Save();
        }

        public void RejectProposal(Guid proposalId, Guid changeInitiatorId, string reason)
        {
            _unitOfWork.Proposals.Reject(proposalId, changeInitiatorId, reason);
            _unitOfWork.Save();
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
