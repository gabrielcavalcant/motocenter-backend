using System;
using System.Collections.Generic;

namespace OficinaMotocenter.Domain.Entities
{
    public class Team : BaseEntity
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }

        // Relacionamento com TeamMember
        public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
    }
}
