using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleWare_2.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        public readonly RequestDelegate next;
        public ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate request,ILogger<ExceptionHandlingMiddleware> _logger)
        {
            next = request;
            logger = _logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch(Exception ex)
            {
                string a = ex.Message;
                logger.LogError(ex.Message);
            }
             
        }
    }
}
