using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleWare_2.Middlewares
{
    public class RequestResponseMiddleware
    {
        public readonly RequestDelegate next;
        public ILogger<RequestResponseMiddleware> logger;

        public RequestResponseMiddleware(RequestDelegate request,ILogger<RequestResponseMiddleware> _logger)
        {
            next = request;
            logger = _logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //request

            string a = string.Format($"1-ci:{httpContext.Request.Path.Value},2-ci:{httpContext.Request.Method},3-cu:{httpContext.Request.QueryString.Value}");

            logger.LogInformation($"1-ci:{httpContext.Request.Path.Value},2-ci:{httpContext.Request.Method},3-cu:{httpContext.Request.QueryString.Value}");

            var tempStream = new MemoryStream();
            httpContext.Response.Body = tempStream;

            await next.Invoke(httpContext);

            string b = string.Format($"1-ci:{httpContext.Response.StatusCode},2-ci:{httpContext.Response.ContentType}");

            httpContext.Response.Body.Seek(0, System.IO.SeekOrigin.Begin);

            string c =await new StreamReader(tempStream,System.Text.Encoding.UTF8).ReadToEndAsync();
        }
    }
}
