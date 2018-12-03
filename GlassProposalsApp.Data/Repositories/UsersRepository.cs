using GlassProposalsApp.Data;
using GlassProposalsApp.Data.Interfaces;
using GlassProposalsApp.Data.Models;
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
            var processFirstStage = Db.Processes.Include(p => p.Stages).FirstOrDefault(p => p.ProcessType == processType) ?
                                                .Stages
                                                .First();
            return Db.Users.Where(u => u.UserType == processFirstStage.ReceiverType);
        }

        public IQueryable<Users> GetDecisionMakersForNextStage(Guid proposalId)
        {
            var proposalNextStage = Db.Proposals.Include(p => p.Process).ThenInclude(p => p.Stages)
                                                       .ThenInclude(p => p.NextStage)
                                                       .Where(p => p.Id == proposalId)
                                                       .Select(p => p.Process.Stages.FirstOrDefault(x => x.Id == p.CurrentStageId))
                                                       .FirstOrDefault()?
                                                       .NextStage; ;

            return Db.Users.Where(u => u.UserType == proposalNextStage.ReceiverType);
        }
    }
}
