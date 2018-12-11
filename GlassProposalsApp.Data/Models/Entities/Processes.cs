using System;
using System.Collections.Generic;

namespace GlassProposalsApp.Data.Entities
{
    public partial class Processes
    {
        public Processes()
        {
            Proposals = new HashSet<Proposals>();
            Stages = new HashSet<Stages>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public int ProcessType { get; set; }

        public ICollection<Proposals> Proposals { get; set; }
        public ICollection<Stages> Stages { get; set; }
    }
}
