using System.Net;
using ApplicationServices.Exceptions;
using Domain.DomaiinException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WalletApi.Extensions;
using WalletApi.Models.Error;

namespace WalletApi.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            HttpStatusCode statusCode;
            ErrorResponse response;
            switch (context.Exception)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    response = notFoundException.ChangeToError();
                    break;
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    response = badRequestException.ChangeToError();
                    break;
                case UnAuthorizedException unauthorized:
                    statusCode = HttpStatusCode.Unauthorized;
                    response = unauthorized.ChangeToError();
                    break;
                case ArgumentIsOutOfRangeException outOfRange:
                    statusCode = HttpStatusCode.BadRequest;
                    response = outOfRange.ChangeToError();
                    break;
                case ArgumentIsNullException argsIsNull:
                    statusCode = HttpStatusCode.BadRequest;
                    response = argsIsNull.ChangeToError();
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    response = context.Exception.ChangeToError();
                    break;
            }

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var serializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };
            var result = JsonConvert.SerializeObject(response, serializerSettings);
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.HttpContext.Response.WriteAsync(result);
            context.ExceptionHandled = true;
        }
    }
}