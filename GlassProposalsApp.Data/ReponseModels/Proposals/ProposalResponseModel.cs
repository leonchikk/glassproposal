
using GlassProposalsApp.Data.ReponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.ReponseModels.Proposals
{
    public class ProposalResponseModel
    {
        public Guid ProposalId { get; set; }
        public UserResponseModel Initiator { get; set; }
        public bool IsUrgently { get; set; }
        public String Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public VacationData VacationData { get; set; }
        public string Title { get; set; }
        public bool IsClosed { get; set; }
        public string RejectReason { get; set; }
    }
}
