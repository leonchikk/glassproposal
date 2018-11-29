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

        public ProposalResponseModel Create(ProposalViewModel model, Guid initiatorId)
        {
            Processes process = _dbContext.Processes.Include(p => p.Stages)
                                          .FirstOrDefault(p => p.ProcessType == model.ProcessType && p.IsPrivate == model.IsPrivate);

            if (process == null)
                throw new Exception("It's process type does not exist");
            
            var proposal = new Proposals(process, initiatorId, model);

            if (model.VacationData != null)
            {
                var vacation = new Vacations(initiatorId, model);
                proposal.VacationId = vacation.Id;

                _dbContext.Vacations.Add(vacation);
            }

            var status = new Statuses(model.DecisionMakerId, proposal.Id);

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
                                                .Where(p => p.Process.IsPrivate == false);

            return _mapper.Map<IEnumerable<Proposals>, IEnumerable<ProposalResponseModel>>(proposals);
        }

        public IEnumerable<ProposalResponseModel> GetUnhandledProposals(Guid UserId)
        {
            var proposals = _dbContext.Statuses.Where(s => s.DecisionMakerId == UserId && s.StatusCode == (int)StatusCodes.None)
                                               .Select(s => s.Proposal)
                                               .Include(p => p.Process)
                                               .Include(p => p.Initiator)
                                               .Include(p => p.Vacation)
                                               .AsNoTracking();

            return _mapper.Map<IEnumerable<Proposals>, IEnumerable<ProposalResponseModel>>(proposals);
        }

        public IEnumerable<ProposalResponseModel> GetUserProposals(Guid UserId)
        {
            var proposals = _dbContext.Proposals.Where(p => p.InitiatorId == UserId)
                                                .Include(p => p.Process)
                                                .Include(p => p.Initiator)
                                                .Include(p => p.Vacation)
                                                .AsNoTracking();


            return _mapper.Map<IEnumerable<Proposals>, IEnumerable<ProposalResponseModel>>(proposals);
        }
    }
}
