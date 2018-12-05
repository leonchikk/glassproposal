using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.ViewModels.Proposals
{
    public class ApproveProposalViewModel
    {
        public Guid ProposalId { get; set; }
        public Guid? NextDecisionMakerId { get; set; }
    }
}
