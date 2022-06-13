using Project_ASP.Domain.Interfaces;
using System.Collections.Generic;

namespace Project_ASP.Api.Core
{
    public class JwtUser : IApplicationUser
    {
        public string Identity { get; set; }
        public int Id { get; set; }
        public IEnumerable<int> PermissionIds { get; set; } = new List<int>();
        public string Email { get; set; }
    }

    public class AnonimousUser : IApplicationUser
    {
        public string Identity => "Anonymous";

        public int Id => 0;

        public IEnumerable<int> PermissionIds => new List<int> { 1, 18, 19 };

        public string Email => "anonimous@gmail.com";
    }
}
