using GlassProposalsApp.Data.ReponseModels.Proposals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GlassProposalsApp.Data.ViewModels.Proposals
{
    public class ProposalViewModel
    {
        [Required(ErrorMessage = "Title cannot be empty!")]
        public string Title { get; set; }
        public VacationData VacationData { get; set; }
        public bool IsUrgently { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Decision maker have to be selected!")]
        public Guid DecisionMakerId { get; set; }

        public bool IsPrivate { get; set; } = false;

        [Required(ErrorMessage = "Process type have to be selected!")]
        public int ProcessType { get; set; }
    }
}
