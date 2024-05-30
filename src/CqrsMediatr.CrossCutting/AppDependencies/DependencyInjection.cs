using CqrsMediatr.Application.Members.Commands.Validations;
using CqrsMediatr.Domain.Abstractions;
using CqrsMediatr.Infrastructure.Context;
using CqrsMediatr.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Reflection;

namespace CqrsMediatr.CrossCutting.AppDependencies
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var sqliteConnection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(sqliteConnection));

            services.AddSingleton<IDbConnection>(provider => {

                var connection = new SqliteConnection(sqliteConnection);
                connection.Open();
                return connection;
            });

            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IUnitOfWork, UnityOfWork>();
            services.AddScoped<IMemberDapperRepository, MemberDapperRepository>();

            var myhandlers = AppDomain.CurrentDomain.Load("CqrsMediatr.Application");
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(myhandlers);
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.Load("CqrsMediatr.Application"));

            return services;
        }
    }
}