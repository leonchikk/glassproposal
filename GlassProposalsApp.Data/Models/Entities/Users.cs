using System;
using System.Collections.Generic;

namespace GlassProposalsApp.Data.Entities
{
    public partial class Users
    {
        public Users()
        {
            Bonuses = new HashSet<Bonuses>();
            Dislikes = new HashSet<Dislikes>();
            InverseMentor = new HashSet<Users>();
            Likes = new HashSet<Likes>();
            Proposals = new HashSet<Proposals>();
            Statuses = new HashSet<Statuses>();
            UserTypes = new HashSet<UserTypes>();
            Vacations = new HashSet<Vacations>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid? MentorId { get; set; }
        public int SecurityLevel { get; set; }
        public int? UserLevel { get; set; }
        public int VacationDaysLeft { get; set; }
        public int BonusBalance { get; set; }

        public Users Mentor { get; set; }
        public ICollection<Bonuses> Bonuses { get; set; }
        public ICollection<Dislikes> Dislikes { get; set; }
        public ICollection<Users> InverseMentor { get; set; }
        public ICollection<Likes> Likes { get; set; }
        public ICollection<Proposals> Proposals { get; set; }
        public ICollection<Statuses> Statuses { get; set; }
        public ICollection<UserTypes> UserTypes { get; set; }
        public ICollection<Vacations> Vacations { get; set; }
    }
}
