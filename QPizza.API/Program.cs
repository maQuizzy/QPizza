
using Microsoft.AspNetCore.Mvc.Infrastructure;
using QPizza.API.Errors;
using QPizza.API.Filters;
using QPizza.API.Middleware;
using QPizza.Application;
using QPizza.Application.Services.Authentication;
using QPizza.Infrastructure;

namespace QPizza.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddApplication()
                .AddInfrastructure(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddSingleton<ProblemDetailsFactory, QPizzaProblemDetailsFactory>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseExceptionHandler("/error");
            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}