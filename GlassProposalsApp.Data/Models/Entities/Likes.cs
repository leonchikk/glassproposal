﻿using System;
using System.Collections.Generic;

namespace GlassProposalsApp.Data.Entities
{
    public partial class Likes
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProposalId { get; set; }

        public Proposals Proposal { get; set; }
        public Users User { get; set; }
    }
}
