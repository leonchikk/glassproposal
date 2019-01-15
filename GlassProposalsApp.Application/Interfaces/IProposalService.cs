using GlassProposalsApp.Data.ReponseModels.Proposals;
using GlassProposalsApp.Data.ViewModels.Proposals;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Application.Interfaces
{
    public interface IProposalService
    {
        ProposalResponseModel CreateVacationProposal(VacationProposalViewModel model, Guid initiatorId);
        ProposalResponseModel CreateCustomProposal(CustomProposalViewModel model, Guid initiatorId);
        ProposalResponseModel CreateSalaryIncreaseProposal(SalaryProposalViewModel model, Guid initiatorId);
        ProposalResponseModel CreateLevelUpProposal(LevelUpViewModel model, Guid initiatorId);
        ProposalResponseModel Like(Guid proposalId, Guid userId);
        ProposalResponseModel Dislike(Guid proposalId, Guid userId);

        IEnumerable<ProposalResponseModel> GetUserProposals(Guid userId);
        IEnumerable<ProposalResponseModel> GetPublicProposals(Guid userId);
        IEnumerable<ProposalResponseModel> GetUnhandledProposals(Guid uUserId);
    }
}
