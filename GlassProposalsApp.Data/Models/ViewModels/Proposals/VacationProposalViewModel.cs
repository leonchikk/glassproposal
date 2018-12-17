using GlassProposalsApp.Data.ReponseModels.Proposals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GlassProposalsApp.Data.ViewModels.Proposals
{
    public class VacationProposalViewModel
    {
        [Required(ErrorMessage = "Vacation data cannot be empty!")]
        public VacationData VacationData { get; set; }

        public string Reason { get; set; }

        [Required(ErrorMessage = "Is urgently value cannot be empty!")]
        public bool IsUrgently { get; set; }

        [Required(ErrorMessage = "Decision maker have to be selected!")]
        public Guid DecisionMakerId { get; set; }
    }
}
