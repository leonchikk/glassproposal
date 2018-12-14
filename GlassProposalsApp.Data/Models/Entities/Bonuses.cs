using System;
using System.Collections.Generic;

namespace GlassProposalsApp.Data.Entities
{
    public partial class Bonuses
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Spent { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public Users User { get; set; }
    }
}
