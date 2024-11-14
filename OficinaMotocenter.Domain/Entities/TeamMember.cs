using OficinaMotocenter.Domain.Entities.Enumerations;
using System;

namespace OficinaMotocenter.Domain.Entities
{
    public class TeamMember : BaseEntity
    {
        public Guid TeamMemberId { get; set; }
        public TeamMemberSpecialty Specialty { get; set; }
        
        // Chaves estrangeiras como Guid
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }

        // Relacionamentos
        public Team Team { get; set; }
        public User User { get; set; }
    }
}
