using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.ViewModels
{
    public class VacationProposalViewModel
    {
        public Guid InitiatorId { get; set; }
        public Guid DecisionMakerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsUrgently { get; set; }
        public bool IsPaidLeave { get; set; }
        public string Description { get; set; }
    }
}
