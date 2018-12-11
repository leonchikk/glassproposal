using GlassProposalsApp.Data.Enumerations;
using GlassProposalsApp.Data.Interfaces;
using GlassProposalsApp.Data.Entities;
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

        public void Approve(Guid proposalId, Guid changeInitiatorId, Guid? nextDecisionMakerId)
        {
            var proposal = Db.Proposals.Include(x => x.Process)
                                       .Include(x => x.Statuses)
                                       .Include(x => x.CurrentStage)
                                            .ThenInclude(x => x.NextStage)
                                       .FirstOrDefault(p => p.Id == proposalId);

            var currentStatus = proposal.Statuses.FirstOrDefault(status => status.DecisionMakerId == changeInitiatorId);

            currentStatus.StatusCode = (int)StatusCodes.Approved;
            currentStatus.UpdatedAt = DateTime.Now;

            Db.Statuses.Update(currentStatus);

            if (proposal.CurrentStage.NextStage == null)
            {
                proposal.IsClosed = true;

                if (proposal.Process.ProcessType == (int)ProcessesTypes.Vacation)
                    Db.Vacations.FirstOrDefault(vacation => vacation.UserId == proposal.InitiatorId).IsApproved = true;
            }
            else
            {
                proposal.CurrentStageId = proposal.CurrentStage.NextStage.Id;
                var status = new Statuses(nextDecisionMakerId.Value, proposal.Id);
                Db.Statuses.Add(status);
            }

            proposal.UpdatedAt = DateTime.Now;
            Db.Proposals.Update(proposal);
        }

        public void Reject(Guid proposalId, Guid changeInitiatorId, string reason)
        {
            var proposal = Db.Proposals.Include(x => x.Process)
                                       .Include(x => x.Statuses)
                                       .Include(x => x.CurrentStage)
                                            .ThenInclude(x => x.NextStage)
                                       .FirstOrDefault(p => p.Id == proposalId);

            var currentStatus = proposal.Statuses.FirstOrDefault(status => status.DecisionMakerId == changeInitiatorId);

            currentStatus.StatusCode = (int)StatusCodes.Rejected;
            currentStatus.UpdatedAt = DateTime.Now;

            Db.Statuses.Update(currentStatus);

            proposal.IsClosed = true;
            proposal.RejectReason = reason;
            proposal.UpdatedAt = DateTime.Now;

            Db.Proposals.Update(proposal);
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

            proposal.Initiator = Db.Users.FirstOrDefault(user => user.Id == initiatorId);

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

            proposal.Initiator = Db.Users.FirstOrDefault(user => user.Id == initiatorId);

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

            proposal.Initiator = Db.Users.FirstOrDefault(user => user.Id == initiatorId);

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

            proposal.Initiator = Db.Users.FirstOrDefault(user => user.Id == initiatorId);

            return proposal;
        }

        public IQueryable<Proposals> GetProposalsByUserId(Guid userId)
        {
            return Db.Proposals.Where(p => p.InitiatorId == userId)
                                                 .Include(p => p.Process)
                                                 .Include(p => p.Initiator)
                                                 .Include(p => p.Vacation)
                                                 .Include(p => p.Likes)
                                                    .ThenInclude(l => l.User)
                                                 .Include(p => p.Dislikes)
                                                    .ThenInclude(d => d.User)
                                                 .OrderByDescending(p => p.CreatedAt)
                                                 .AsNoTracking();
        }

        public IQueryable<Proposals> GetPublicProposals()
        {
            return Db.Proposals.Include(p => p.Process)
                                               .Include(p => p.Initiator)
                                               .Include(p => p.Vacation)
                                               .Include(p => p.Likes)
                                                    .ThenInclude(l => l.User)
                                               .Include(p => p.Dislikes)
                                                    .ThenInclude(d => d.User)
                                               .Where(p => p.Process.IsPrivate == false)
                                               .OrderByDescending(p => p.CreatedAt)
                                               .AsNoTracking();
        }

        public IQueryable<Proposals> GetUnHandledProposalsByUserId(Guid userId)
        {
            return Db.Statuses.Where(s => s.DecisionMakerId == userId && s.StatusCode == (int)StatusCodes.None)
                                               .Select(s => s.Proposal)
                                               .Include(p => p.Process)
                                               .Include(p => p.Initiator)
                                               .Include(p => p.Vacation)
                                               .Include(p => p.Likes)
                                                    .ThenInclude(l => l.User)
                                               .Include(p => p.Dislikes)
                                                    .ThenInclude(d => d.User)
                                               .OrderByDescending(p => p.CreatedAt)
                                               .AsNoTracking();
        }

        public void Like(Guid proposalId, Guid userId)
        {
            var dislike = Db.Dislikes.FirstOrDefault(d => d.ProposalId == proposalId && d.UserId == userId);

            if (dislike != null)
                Db.Dislikes.Remove(dislike);

            var like = new Likes(proposalId, userId);
            Db.Likes.Add(like);
        }

        public void Dislike(Guid proposalId, Guid userId)
        {
            var like = Db.Likes.FirstOrDefault(d => d.ProposalId == proposalId && d.UserId == userId);

            if (like != null)
                Db.Likes.Remove(like);

            var dislike = new Dislikes(proposalId, userId);
            Db.Dislikes.Add(dislike);
        }

        public Proposals GetById(Guid Id)
        {
            return Db.Proposals.Include(p => p.Process)
                               .Include(p => p.Initiator)
                               .Include(p => p.Vacation)
                               .Include(p => p.Likes)
                                    .ThenInclude(l => l.User)
                               .Include(p => p.Dislikes)
                                    .ThenInclude(d => d.User)
                               .FirstOrDefault(p => p.Id == Id);
        }
    }
}
