using Project_ASP.Domain.Enums;
using System;

namespace Project_ASP.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public eEntityStatus EntityStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}
