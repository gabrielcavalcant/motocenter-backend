using System;

namespace OficinaMotocenter.Domain.Entities
{
    public class TeamMember : BaseEntity
    {
        public Guid Id { get; set; }
        public string Specialty { get; set; }
        
        // Chaves estrangeiras como Guid
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }

        // Relacionamentos
        public Team Team { get; set; }
        public User User { get; set; }
    }
}
