using System;
using System.Collections.Generic;

namespace GlassProposalsApp.Data.Models
{
    public partial class Vacations
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPaidLeave { get; set; }
        public Guid UserId { get; set; }
        public bool IsApproved { get; set; }

        public Users User { get; set; }
    }
}
