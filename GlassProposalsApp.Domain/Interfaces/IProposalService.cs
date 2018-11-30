using GlassProposalsApp.Data.ReponseModels.Proposals;
using GlassProposalsApp.Data.ViewModels.Proposals;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Domain.Interfaces
{
    public interface IProposalService
    {
        ProposalResponseModel CreateVacationProposal(VacationProposalViewModel model, Guid initiatorId);
        ProposalResponseModel CreateCustomProposal(CustomProposalViewModel model, Guid initiatorId);
        ProposalResponseModel CreateSalaryIncreaseProposal(SalaryProposalViewModel model, Guid initiatorId);
        ProposalResponseModel CreateLevelUpProposal(LevelUpViewModel model, Guid initiatorId);

        IEnumerable<ProposalResponseModel> GetUserProposals(Guid UserId);
        IEnumerable<ProposalResponseModel> GetPublicProposals();
        IEnumerable<ProposalResponseModel> GetUnhandledProposals(Guid UserId);
    }
}
