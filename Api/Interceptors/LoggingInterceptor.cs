using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Api.Interceptors
{
    public class LoggingInterceptor : Interceptor
    {
        private ILogger<LoggingInterceptor> _logger;
        public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request, ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
        {
            _logger.LogInformation($"Start method: {context.Method}. Request: {request}");
            try
            {
                var response = await continuation(request, context);
                _logger.LogInformation($"Method {context.Method} ended successfully. Responce: {response}");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method {context.Method} ended with failure. Exception: {ex.Message}");
                throw;
            }
        }
    }
}
