using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.ReponseModels.Dashboard
{
    public class UserResponseModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public Guid MentorId { get; set; }
        public int UserLevel { get; set; }
        public int VacationDaysLeft { get; set; }
        public int BonusBalance { get; set; }
        public IEnumerable<int> UserTypes { get; set; }
    }
}
