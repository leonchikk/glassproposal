using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GlassProposalsApp.Data.ViewModels.Proposals
{
    public class CustomProposalViewModel
    {
        [Required(ErrorMessage = "Title cannot be empty!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Title cannot be empty!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Is public value cannot be empty!")]
        public bool IsPublic { get; set; }
    }
}
