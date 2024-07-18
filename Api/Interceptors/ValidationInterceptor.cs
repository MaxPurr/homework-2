using FluentValidation;
using FluentValidation.Results;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Api.Interceptors
{
    public class ValidationInterceptor : Interceptor
    {
        private IServiceProvider _serviceProvider;
        public ValidationInterceptor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            var validator = _serviceProvider.GetService<IValidator<TRequest>>();
            ValidationResult? validationResult = validator?.Validate(request);
            if (validationResult is not null && !validationResult.IsValid)
            {
                string errors = $"{string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))}";
                throw new RpcException(new Status(StatusCode.InvalidArgument, errors));
            }
            return continuation(request, context);
        }
    }
}
