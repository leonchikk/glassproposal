using System;
using System.Collections.Generic;

namespace GlassProposalsApp.Data.Entities
{
    public partial class Stages
    {
        public Stages()
        {
            InverseNextStage = new HashSet<Stages>();
            Proposals = new HashSet<Proposals>();
            StageReceivers = new HashSet<StageReceivers>();
        }

        public Guid Id { get; set; }
        public Guid ProcessId { get; set; }
        public Guid? NextStageId { get; set; }

        public Stages IdNavigation { get; set; }
        public Stages NextStage { get; set; }
        public Processes Process { get; set; }
        public Stages InverseIdNavigation { get; set; }
        public ICollection<Stages> InverseNextStage { get; set; }
        public ICollection<Proposals> Proposals { get; set; }
        public ICollection<StageReceivers> StageReceivers { get; set; }
    }
}
