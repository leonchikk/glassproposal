using GlassProposalsApp.Data.Enumerations;
using GlassProposalsApp.Data.Interfaces;
using GlassProposalsApp.Data.Models;
using GlassProposalsApp.Data.ViewModels.Proposals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Data.Repositories
{
    public class ProposalRepository : Repository<Proposals>, IProposalRepository
    {
        public ProposalRepository(GlassProposalContext context)
              : base(context)
        {

        }

        public Proposals CreateCustomProposal(CustomProposalViewModel model, Guid initiatorId)
        {
            Processes process = Db.Processes.Include(p => p.Stages)
                                        .ThenInclude(s => s.StageReceivers)
                                        .First(p => p.ProcessType == (int)ProcessesTypes.Custom && p.IsPrivate != model.IsPublic);

            var receiversTypes = process.Stages.First().StageReceivers.Select(s => s.ReceiverType);

            var decisionMaker = Db.Users.Include(user => user.UserTypes)
                                        .First(u => u.UserTypes.Any(x => receiversTypes.Contains(x.UserType)));

            var proposal = new Proposals(process, initiatorId, model.Description, model.Title);
            var status = new Statuses(decisionMaker.Id, proposal.Id);

            Db.Proposals.Add(proposal);
            Db.Statuses.Add(status);

            return proposal;
        }

        public Proposals CreateLevelUpProposal(LevelUpViewModel model, Guid initiatorId)
        {
            Processes process = Db.Processes.Include(p => p.Stages)
                                          .First(p => p.ProcessType == (int)ProcessesTypes.LevelUp);

            var proposal = new Proposals(process, initiatorId, title: process.Name);
            var status = new Statuses(model.DecisionMakerId, proposal.Id);

            Db.Proposals.Add(proposal);
            Db.Statuses.Add(status);

            return proposal;
        }

        public Proposals CreateSalaryIncreaseProposal(SalaryProposalViewModel model, Guid initiatorId)
        {
            Processes process = Db.Processes.Include(p => p.Stages)
                                         .First(p => p.ProcessType == (int)ProcessesTypes.SalaryIncrease);

            var proposal = new Proposals(process, initiatorId, title: process.Name);
            var status = new Statuses(model.DecisionMakerId, proposal.Id);

            Db.Proposals.Add(proposal);
            Db.Statuses.Add(status);

            return proposal;
        }

        public Proposals CreateVacationProposal(VacationProposalViewModel model, Guid initiatorId)
        {
            Processes process = Db.Processes.Include(p => p.Stages)
                                       .First(p => p.ProcessType == (int)ProcessesTypes.Vacation);

            var vacation = new Vacations(initiatorId, model);
            var proposal = new Proposals(process, initiatorId, title: process.Name, isUrgently: model.IsUrgently, vacationId: vacation.Id);
            var status = new Statuses(model.DecisionMakerId, proposal.Id);

            Db.Vacations.Add(vacation);
            Db.Proposals.Add(proposal);
            Db.Statuses.Add(status);

            return proposal;
        }

        public IQueryable<Proposals> GetProposalsByUserId(Guid userId)
        {
            return Db.Proposals.Where(p => p.InitiatorId == userId)
                                                 .Include(p => p.Process)
                                                 .Include(p => p.Initiator)
                                                 .Include(p => p.Vacation)
                                                 .OrderBy(p => p.CreatedAt)
                                                 .AsNoTracking();
        }

        public IQueryable<Proposals> GetPublicProposals()
        {
            return Db.Proposals.Include(p => p.Process)
                                               .Include(p => p.Initiator)
                                               .Include(p => p.Vacation)
                                               .Where(p => p.Process.IsPrivate == false)
                                               .OrderBy(p => p.CreatedAt)
                                               .AsNoTracking();
        }

        public IQueryable<Proposals> GetUnHandledProposalsByUserId(Guid userId)
        {
            return Db.Statuses.Where(s => s.DecisionMakerId == userId && s.StatusCode == (int)StatusCodes.None)
                                               .Select(s => s.Proposal)
                                               .Include(p => p.Process)
                                               .Include(p => p.Initiator)
                                               .Include(p => p.Vacation)
                                               .OrderBy(p => p.CreatedAt)
                                               .AsNoTracking();
        }
    }
}
