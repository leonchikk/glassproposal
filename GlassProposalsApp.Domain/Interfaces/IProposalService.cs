using GlassProposalsApp.Data.ReponseModels.Proposals;
using GlassProposalsApp.Data.ViewModels.Proposals;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Domain.Interfaces
{
    public interface IProposalService
    {
        ProposalResponseModel Create(ProposalViewModel model, Guid initiatorId);
        IEnumerable<ProposalResponseModel> GetUserProposals(Guid UserId);
        IEnumerable<ProposalResponseModel> GetPublicProposals();
        IEnumerable<ProposalResponseModel> GetUnhandledProposals(Guid UserId);
    }
}
