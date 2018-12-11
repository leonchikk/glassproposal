using GlassProposalsApp.Data;
using GlassProposalsApp.Data.Entities;
using GlassProposalsApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Data.Repositories
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        public UsersRepository(GlassProposalContext context)
             : base(context)
        {

        }

        public IQueryable<Users> GetDecisionMakersForFirstStage(int processType)
        {
            var processFirstStage = Db.Processes.Include(p => p.Stages)
                                                .ThenInclude(s => s.StageReceivers)
                                                .FirstOrDefault(p => p.ProcessType == processType) ?
                                                .Stages
                                                .First();

            if (processFirstStage == null)
                return Enumerable.Empty<Users>().AsQueryable();

            var receiversTypes = processFirstStage.StageReceivers.Select(s => s.ReceiverType);
            var decisionMakers = Db.Users.Include(user => user.UserTypes).Where(u => u.UserTypes.Any(x => receiversTypes.Contains(x.UserType)));

            return decisionMakers;
        }

        public IQueryable<Users> GetDecisionMakersForNextStage(Guid proposalId)
        {
            var proposalNextStage = Db.Proposals.Include(proposal => proposal.CurrentStage)
                                                .ThenInclude(stage => stage.NextStage)
                                                .ThenInclude(s => s.StageReceivers)
                                                .FirstOrDefault(proposal => proposal.Id == proposalId)?
                                                .CurrentStage
                                                .NextStage;

            if (proposalNextStage == null)
                return Enumerable.Empty<Users>().AsQueryable();

            var receiversTypes = proposalNextStage.StageReceivers.Select(s => s.ReceiverType);
            var decisionMakers = Db.Users.Include(user => user.UserTypes).Where(u => u.UserTypes.Any(x => receiversTypes.Contains(x.UserType)));

            return decisionMakers;
        }
    }
}
