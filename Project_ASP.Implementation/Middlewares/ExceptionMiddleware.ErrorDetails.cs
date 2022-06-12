using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.Middlewares
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Errors { get; set; }

        public ErrorDetails()
        {
            Errors = null;
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
