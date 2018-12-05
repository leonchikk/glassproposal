using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.Models
{
    public partial class Dislikes
    {
        public Dislikes(Guid proposalId, Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ProposalId = proposalId;
        }
    }
}
