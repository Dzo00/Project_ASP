using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.BusinessLogic
{
    public interface IBaseQuery<TRequest, TResult> : IUseCase
    {
        TResult Execute(TRequest search);
    }
}
