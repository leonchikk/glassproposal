using System;
using System.Collections.Generic;

namespace GlassProposalsApp.Data.Models
{
    public partial class Proposals
    {
        public Proposals()
        {
            Dislikes = new HashSet<Dislikes>();
            Likes = new HashSet<Likes>();
            Statuses = new HashSet<Statuses>();
        }

        public Guid Id { get; set; }
        public Guid ProcessId { get; set; }
        public Guid InitiatorId { get; set; }
        public Guid? CurrentStageId { get; set; }
        public bool IsUrgently { get; set; }
        public bool IsClosed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Description { get; set; }
        public Guid? VacationId { get; set; }
        public string RejectReason { get; set; }
        public string Title { get; set; }

        public Stages CurrentStage { get; set; }
        public Users Initiator { get; set; }
        public Processes Process { get; set; }
        public Vacations Vacation { get; set; }
        public ICollection<Dislikes> Dislikes { get; set; }
        public ICollection<Likes> Likes { get; set; }
        public ICollection<Statuses> Statuses { get; set; }
    }
}
