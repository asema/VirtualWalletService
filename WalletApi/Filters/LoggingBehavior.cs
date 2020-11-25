using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace WalletApi.Filters
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //Request
            _logger.LogInformation("------Handling {RequestType}  ({@Request})", typeof(TRequest).Name, request);

            var response = await next();
            //Response
            _logger.LogInformation("------Handled {ResponseType}  ({@Response})", typeof(TRequest).Name, response);
            return response;
        }
    }
}
