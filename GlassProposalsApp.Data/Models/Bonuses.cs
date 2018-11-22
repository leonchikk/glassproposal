using System;
using System.Collections.Generic;

namespace GlassProposalsApp.Data.Models
{
    public partial class Bonuses
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Spent { get; set; }
        public string Description { get; set; }

        public Users User { get; set; }
    }
}
