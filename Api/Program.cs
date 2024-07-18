using Api.Interceptors;
using Api.Services;
using DataAccess;
using Domain.Services;
using FluentValidation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddGrpc(o =>
        {
            o.Interceptors.Add<LoggingInterceptor>();
            o.Interceptors.Add<ExceptionsInterceptor>();
            o.Interceptors.Add<ValidationInterceptor>();
        }).AddJsonTranscoding();

        builder.Services.AddGrpcSwagger();
        builder.Services.AddSwaggerGen();
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));
        builder.Services.AddServices();
        builder.Services.AddRepositories();

        builder.Services.Configure<IDisposable>(builder.Configuration.GetSection(""));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapGrpcService<ProductGrpcService>();

        app.Run();
    }
}
