using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Domain.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<RolePermission> Permissions { get; set; } = new List<RolePermission>();
    }
}
