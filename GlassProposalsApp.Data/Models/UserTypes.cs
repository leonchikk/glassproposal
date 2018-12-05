using System;
using System.Collections.Generic;

namespace GlassProposalsApp.Data.Models
{
    public partial class UserTypes
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int UserType { get; set; }

        public Users User { get; set; }
    }
}
