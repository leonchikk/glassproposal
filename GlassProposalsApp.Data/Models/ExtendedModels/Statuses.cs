using GlassProposalsApp.Data.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.Entities
{
    public partial class Statuses
    {
        public Statuses(Guid decisionMakerId, Guid proposalId, int statusCode = (int)StatusCodes.None)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            DecisionMakerId = decisionMakerId;
            StatusCode = statusCode;
            ProposalId = proposalId;
        }
    }
}
