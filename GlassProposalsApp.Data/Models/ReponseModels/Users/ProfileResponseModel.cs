using GlassProposalsApp.Data.ReponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.Models.Users
{
    public class ProfileResponseModel
    {
        public int UserLevel { get; set; }
        public int VacationDaysLeft { get; set; }
        public int BonusBalance { get; set; }
        public UserResponseModel Mentor { get; set; }
        public UserResponseModel User { get; set; }
    }
}
