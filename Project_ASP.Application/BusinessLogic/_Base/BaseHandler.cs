using Newtonsoft.Json;
using Project_ASP.Application.Exceptions;
using Project_ASP.Application.Logger;
using Project_ASP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.BusinessLogic
{
    public class BaseHandler
    {
        private IUseCaseLogger _logger;
        private IApplicationUser _user;

        public BaseHandler(IUseCaseLogger logger, IApplicationUser user)
        {
            _logger = logger;
            _user = user;
        }

        public void HandleCommand<TRequest>(IBaseCommand<TRequest> command, TRequest data)
        {
            try
            {
                HandleLog(command, data);
                command.Execute(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public TResponse HandleQuery<TRequest, TResponse>(IBaseQuery<TRequest, TResponse> query, TRequest data)
        {
            try
            {
                HandleLog(query, data);
                var response = query.Execute(data);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void HandleLog<TRequest>(IUseCase useCase, TRequest data)
        {
            var log = new UseCaseLog
            {
                Username = _user.Identity,
                ExecutedOn = DateTime.UtcNow,
                ActionName = useCase.Name,
                UserId = _user.Id,
                Data = JsonConvert.SerializeObject(data),
                IsAuthorized = isAuthorized(useCase.Id)
            };

            _logger.Log(log);

            if (!isAuthorized(useCase.Id))
            {
                throw new UnauthorizedException();
            }
        }

        private bool isAuthorized(int id) => _user.PermissionIds.Contains(id); 
    }
}
