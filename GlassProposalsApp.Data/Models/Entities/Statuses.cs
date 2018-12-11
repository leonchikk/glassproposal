using System;
using System.Collections.Generic;

namespace GlassProposalsApp.Data.Entities
{
    public partial class Statuses
    {
        public Guid Id { get; set; }
        public Guid ProposalId { get; set; }
        public int StatusCode { get; set; }
        public Guid DecisionMakerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Users DecisionMaker { get; set; }
        public Proposals Proposal { get; set; }
    }
}
