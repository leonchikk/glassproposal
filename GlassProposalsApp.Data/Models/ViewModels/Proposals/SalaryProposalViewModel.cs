using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GlassProposalsApp.Data.ViewModels.Proposals
{
    public class SalaryProposalViewModel
    {
        public string Description { get; set; }

        [Required(ErrorMessage = "Decision maker have to be selected!")]
        public Guid DecisionMakerId { get; set; }
    }
}
