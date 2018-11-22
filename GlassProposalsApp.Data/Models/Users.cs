using System;
using System.Collections.Generic;

namespace GlassProposalsApp.Data.Models
{
    public partial class Users
    {
        public Users()
        {
            Bonuses = new HashSet<Bonuses>();
            InverseMentor = new HashSet<Users>();
            Proposals = new HashSet<Proposals>();
            Statuses = new HashSet<Statuses>();
            Vacations = new HashSet<Vacations>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid? MentorId { get; set; }
        public int UserType { get; set; }
        public int SecurityLevel { get; set; }

        public Users Mentor { get; set; }
        public ICollection<Bonuses> Bonuses { get; set; }
        public ICollection<Users> InverseMentor { get; set; }
        public ICollection<Proposals> Proposals { get; set; }
        public ICollection<Statuses> Statuses { get; set; }
        public ICollection<Vacations> Vacations { get; set; }
    }
}
