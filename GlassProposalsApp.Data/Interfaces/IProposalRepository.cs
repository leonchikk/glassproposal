using GlassProposalsApp.Data.Models;
using GlassProposalsApp.Data.ViewModels.Proposals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Domain.Interfaces
{
    public interface IProposalRepository
    {
        IQueryable<Proposals> GetPublicProposals();
        IQueryable<Proposals> GetUnHandledProposalsByUserId(Guid userId);
        IQueryable<Proposals> GetProposalsByUserId(Guid userId);
        Proposals CreateCustomProposal(CustomProposalViewModel model, Guid initiatorId);
        Proposals CreateLevelUpProposal(LevelUpViewModel model, Guid initiatorId);
        Proposals CreateSalaryIncreaseProposal(SalaryProposalViewModel model, Guid initiatorId);
        Proposals CreateVacationProposal(VacationProposalViewModel model, Guid initiatorId);
    }
}
