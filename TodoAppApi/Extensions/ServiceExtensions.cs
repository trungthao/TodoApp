using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using TodoApp.Contracts;
using TodoApp.LoggerService;
using TodoApp.Presentation.ActionFilters;
using TodoApp.Repository;
using TodoApp.Service;
using TodoApp.Service.Contracts;
using TodoApp.Service.DataShaping;
using TodoApp.Shared.DataTransferObjects;
using TodoAppApi.Formatter;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace TodoApp.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("X-Pagination")
            );
        });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureDataShaper(this IServiceCollection services)
        {
            services.AddScoped<IDataShaper<TaskItemDto>, DataShaper<TaskItemDto>>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("SqlConnection"))
            );

        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
            builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));

        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<ValidateMediaTypeAttribute>();
        }

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt => {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }

        public static void AddCustomMediaType(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var systemTextJsonOutputFormatter = config.OutputFormatters
                    .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

                if (systemTextJsonOutputFormatter != null)
                {
                    systemTextJsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.codemaze.hateoas+json");

                    systemTextJsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.codemaze.apiroot+json");
                }

                var xmlOutputFormatter = config.OutputFormatters
                    .OfType<XmlDataContractSerializerOutputFormatter>()?
                    .FirstOrDefault();
                if (xmlOutputFormatter != null)
                {
                    xmlOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.codemaze.hateoas+xml");

                    xmlOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.codemaze.apiroot+xml");
                }
            });
        }
    }
}

