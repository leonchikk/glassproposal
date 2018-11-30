using GlassProposalsApp.Data.ViewModels.Proposals;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.Models
{
    public partial class Vacations
    {
        public Vacations(Guid initiatorId, VacationProposalViewModel model, bool isPaidLeave = true)
        {
            Id = Guid.NewGuid();
            IsApproved = false;
            IsPaidLeave = isPaidLeave;
            EndDate = model.VacationData.EndDate;
            StartDate = model.VacationData.StartDate;
            UserId = initiatorId;
            Reason = model.VacationData.Reason;
        }
    }
}
