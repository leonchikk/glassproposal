using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.Entities
{
    public partial class Likes
    {
        public Likes(Guid proposalId, Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ProposalId = proposalId;
        }
    }
}
