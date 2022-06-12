using Project_ASP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Domain.Interfaces
{
    public interface IAuditedEntity
    {
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        DateTime CreatedOn{ get; set; }
        DateTime ModifiedOn{ get; set; }
        eEntityStatus EntityStatus { get; set; }
    }
}
