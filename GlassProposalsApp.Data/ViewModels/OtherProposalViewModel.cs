using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.ViewModels
{
    public class OtherProposalViewModel
    {
        public Guid InitiatorId { get; set; }
        public Guid DecisionMakerId { get; set; }
        public bool IsUrgently { get; set; }
        public bool IsPrivate { get; set; }
        public string Description { get; set; }
    }
}
