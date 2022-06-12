using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Domain.Entities
{
    public class Permission : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<RolePermission> Roles { get; set; } = new List<RolePermission>();
    }
}
