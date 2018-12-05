using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.ViewModels.Proposals
{
    public class RejectProposalViewModel
    {
        public Guid ProposalId { get; set; }
        public string Reason { get; set; }
    }
}
