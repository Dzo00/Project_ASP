using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.BusinessLogic
{
    public interface IUseCase
    {
        public int Id { get; }
        string Name { get; }
        string Description { get; }
    }
}
