using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Domain.Interfaces
{
    public interface IApplicationUser
    {
        public string Identity { get; }
        public int Id { get; }
        public IEnumerable<int> PermissionIds { get; }
        public string Email { get; }
    }
}
