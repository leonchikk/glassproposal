using System;
using System.Collections.Generic;

namespace GlassProposalsApp.Data.Models
{
    public partial class Proposals
    {
        public Proposals()
        {
            Statuses = new HashSet<Statuses>();
        }

        public Guid Id { get; set; }
        public Guid ProcessId { get; set; }
        public Guid InitiatorId { get; set; }
        public Guid CurrentStageId { get; set; }
        public bool IsUrgently { get; set; }
        public bool IsClosed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Stages CurrentStage { get; set; }
        public Users Initiator { get; set; }
        public Processes Process { get; set; }
        public ICollection<Statuses> Statuses { get; set; }
    }
}
