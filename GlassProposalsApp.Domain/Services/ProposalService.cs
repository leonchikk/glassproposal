using AutoMapper;
using GlassProposalsApp.Data;
using GlassProposalsApp.Data.Enumerations;
using GlassProposalsApp.Data.Models;
using GlassProposalsApp.Data.ReponseModels.Proposals;
using GlassProposalsApp.Data.ViewModels.Proposals;
using GlassProposalsApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Domain.Services
{
    public class ProposalService : IProposalService
    {
        private readonly GlassProposalContext _dbContext;
        private readonly IMapper _mapper;

        public ProposalService(GlassProposalContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ProposalResponseModel CreateCustomProposal(CustomProposalViewModel model, Guid initiatorId)
        {
            Processes process = _dbContext.Processes.Include(p => p.Stages)
                                          .First(p => p.ProcessType == (int)ProcessesTypes.Custom && p.IsPrivate != model.IsPublic);

            var decisionMaker = _dbContext.Users.First(u => u.UserType == process.Stages.First().ReceiverType);

            var proposal = new Proposals(process, initiatorId, model.Description, model.Title);
            var status = new Statuses(decisionMaker.Id, proposal.Id);

            _dbContext.Proposals.Add(proposal);
            _dbContext.Statuses.Add(status);
            _dbContext.SaveChanges();

            return _mapper.Map<Proposals, ProposalResponseModel>(proposal);
        }

        public ProposalResponseModel CreateLevelUpProposal(LevelUpViewModel model, Guid initiatorId)
        {
            Processes process = _dbContext.Processes.Include(p => p.Stages)
                                         .First(p => p.ProcessType == (int)ProcessesTypes.LevelUp);

            var proposal = new Proposals(process, initiatorId, title: process.Name);
            var status = new Statuses(model.DecisionMakerId, proposal.Id);

            _dbContext.Proposals.Add(proposal);
            _dbContext.Statuses.Add(status);
            _dbContext.SaveChanges();

            return _mapper.Map<Proposals, ProposalResponseModel>(proposal);
        }

        public ProposalResponseModel CreateSalaryIncreaseProposal(SalaryProposalViewModel model, Guid initiatorId)
        {
            Processes process = _dbContext.Processes.Include(p => p.Stages)
                                         .First(p => p.ProcessType == (int)ProcessesTypes.SalaryIncrease);

            var proposal = new Proposals(process, initiatorId, title: process.Name);
            var status = new Statuses(model.DecisionMakerId, proposal.Id);

            _dbContext.Proposals.Add(proposal);
            _dbContext.Statuses.Add(status);
            _dbContext.SaveChanges();

            return _mapper.Map<Proposals, ProposalResponseModel>(proposal);
        }

        public ProposalResponseModel CreateVacationProposal(VacationProposalViewModel model, Guid initiatorId)
        {
            Processes process = _dbContext.Processes.Include(p => p.Stages)
                                        .First(p => p.ProcessType == (int)ProcessesTypes.Vacation);

            var vacation = new Vacations(initiatorId, model);
            var proposal = new Proposals(process, initiatorId, title: process.Name, isUrgently: model.IsUrgently, vacationId: vacation.Id);
            var status = new Statuses(model.DecisionMakerId, proposal.Id);

            _dbContext.Vacations.Add(vacation);
            _dbContext.Proposals.Add(proposal);
            _dbContext.Statuses.Add(status);
            _dbContext.SaveChanges();

            return _mapper.Map<Proposals, ProposalResponseModel>(proposal);
        }

        public IEnumerable<ProposalResponseModel> GetPublicProposals()
        {
            var proposals = _dbContext.Proposals.Include(p => p.Process)
                                                .Include(p => p.Initiator)
                                                .Include(p => p.Vacation)
                                                .Where(p => p.Process.IsPrivate == false)
                                                .OrderBy(p => p.CreatedAt);

            return _mapper.Map<IEnumerable<Proposals>, IEnumerable<ProposalResponseModel>>(proposals);
        }

        public IEnumerable<ProposalResponseModel> GetUnhandledProposals(Guid UserId)
        {
            var proposals = _dbContext.Statuses.Where(s => s.DecisionMakerId == UserId && s.StatusCode == (int)StatusCodes.None)
                                               .Select(s => s.Proposal)
                                               .Include(p => p.Process)
                                               .Include(p => p.Initiator)
                                               .Include(p => p.Vacation)
                                               .OrderBy(p => p.CreatedAt)
                                               .AsNoTracking();

            return _mapper.Map<IEnumerable<Proposals>, IEnumerable<ProposalResponseModel>>(proposals);
        }

        public IEnumerable<ProposalResponseModel> GetUserProposals(Guid UserId)
        {
            var proposals = _dbContext.Proposals.Where(p => p.InitiatorId == UserId)
                                                .Include(p => p.Process)
                                                .Include(p => p.Initiator)
                                                .Include(p => p.Vacation)
                                                .OrderBy(p => p.CreatedAt)
                                                .AsNoTracking();


            return _mapper.Map<IEnumerable<Proposals>, IEnumerable<ProposalResponseModel>>(proposals);
        }
    }
}
