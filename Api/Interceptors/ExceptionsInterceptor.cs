using Domain.Exceptions;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Api.Interceptors
{
    public class ExceptionsInterceptor : Interceptor
    {
        public async override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                var response = await continuation(request, context);
                return response;
            }
            catch (RpcException)
            {
                throw;
            }
            catch (NotFoundException ex)
            {
                var rpcException = new RpcException(new Status(StatusCode.NotFound, ex.Message));
                throw rpcException;
            }
            catch (AbortedException ex)
            {
                var rpcException = new RpcException(new Status(StatusCode.Aborted, ex.Message));
                throw rpcException;
            }
            catch (Exception ex)
            {
                var rpcException = new RpcException(new Status(StatusCode.Internal, ex.Message));
                throw rpcException;
            }
        }
    }
}
