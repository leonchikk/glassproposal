using System;
using System.Collections.Generic;

namespace GlassProposalsApp.Data.Models
{
    public partial class Stages
    {
        public Stages()
        {
            InverseNextStage = new HashSet<Stages>();
            Proposals = new HashSet<Proposals>();
        }

        public Guid Id { get; set; }
        public Guid ProcessId { get; set; }
        public int ReceiverType { get; set; }
        public Guid NextStageId { get; set; }

        public Stages NextStage { get; set; }
        public Processes Process { get; set; }
        public ICollection<Stages> InverseNextStage { get; set; }
        public ICollection<Proposals> Proposals { get; set; }
    }
}
