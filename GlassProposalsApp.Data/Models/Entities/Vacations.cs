using System;
using System.Collections.Generic;

namespace GlassProposalsApp.Data.Entities
{
    public partial class Vacations
    {
        public Vacations()
        {
            Proposals = new HashSet<Proposals>();
        }

        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPaidLeave { get; set; }
        public Guid UserId { get; set; }
        public bool IsApproved { get; set; }
        public string Reason { get; set; }
        public int Duration { get; set; }

        public Users User { get; set; }
        public ICollection<Proposals> Proposals { get; set; }
    }
}
