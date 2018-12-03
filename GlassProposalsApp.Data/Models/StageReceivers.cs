using System;
using System.Collections.Generic;

namespace GlassProposalsApp.Data.Models
{
    public partial class StageReceivers
    {
        public Guid Id { get; set; }
        public Guid StageId { get; set; }
        public int ReceiverType { get; set; }

        public Stages Stage { get; set; }
    }
}
