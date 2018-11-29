using AutoMapper;
using GlassProposalsApp.Data;
using GlassProposalsApp.Data.Models;
using GlassProposalsApp.Data.ReponseModels;
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
        private readonly GlassProposalContext _dbContext;
        private readonly IMapper _mapper;

        public DecisionService(GlassProposalContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<UserResponseModel> GetDecisionMakersForFirstStage(int processType)
        {
            IEnumerable<Users> receivers = new HashSet<Users>();

            var processFirstStage = _dbContext.Processes.Include(p => p.Stages)
                                                        .FirstOrDefault(p => p.ProcessType == processType)?
                                                        .Stages
                                                        .First();

            if(processFirstStage != null)
                receivers = _dbContext.Users.Where(u => u.UserType == processFirstStage.ReceiverType);

            return _mapper.Map<IEnumerable<UserResponseModel>>(receivers);
        }

        public IEnumerable<UserResponseModel> GetDecisionMakersForNextStage(Guid proposalId)
        {
            IEnumerable<Users> receivers = new HashSet<Users>();

            var proposalNextStage = _dbContext.Proposals.Include(p => p.Process)
                                                            .ThenInclude(p => p.Stages)
                                                            .ThenInclude(p => p.NextStage)
                                                            .Where(p => p.Id == proposalId)
                                                            .Select(p => p.Process.Stages.FirstOrDefault(x => x.Id == p.CurrentStageId))
                                                            .FirstOrDefault()?
                                                            .NextStage;

            if (proposalNextStage != null)
                receivers = _dbContext.Users.Where(u => u.UserType == proposalNextStage.ReceiverType);

            return _mapper.Map<IEnumerable<UserResponseModel>>(receivers);
        }
    }
}
