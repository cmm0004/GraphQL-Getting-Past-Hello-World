using GraphQL;
using GraphQL.Authorization;
using GraphQL.Execution;
using GraphQL.Server;
using GraphQL.Types;
using GraphQL.Validation;
using GraphQLValidation.Data;
using GraphQLValidation.GraphQl;
using GraphQLValidation.GraphQl.DocumentListeners;
using GraphQLValidation.GraphQl.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLValidation
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            _env = env;
        }


        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddHealthChecks();
            services.AddControllers();

            services.AddEntityFrameworkSqlite()
                .AddDbContext<IContext, Context>(ops => ops.UseSqlite(@"Data Source=.\Data\Data.db"),
                    ServiceLifetime.Transient);


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //grahql stuff
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddSingleton<ISchema, RootSchema>();

            services.AddGraphQL(o =>
                {
                    o.EnableMetrics = true;
                    o.ExposeExceptions = !_env.IsProduction();
                })
                .AddGraphTypes();

            // attribute based auth
            services.AddScoped<IValidationRule, ProductIdValidator>();
            services.AddScoped<IDocumentExecutionListener, AttributeBasedAuthDocumentListener>();

            services.TryAddSingleton(s =>
            {
                var authSettings = new AuthorizationSettings();

                authSettings.AddPolicy("AdminPolicy", _ => _.RequireClaim("role", "Admin"));

                return authSettings;
            });

            services.AddScoped<ExecutionOptions>(s =>
            {
                var ops = new ExecutionOptions
                {
                    Schema = s.GetRequiredService<ISchema>(),
                    ValidationRules = s.GetRequiredService<IEnumerable<IValidationRule>>()
                        .Concat(DocumentValidator.CoreRules()),
                    EnableMetrics = true,
                    ExposeExceptions = !_env.IsProduction()
                };
                foreach (var listener in s.GetRequiredService<IEnumerable<IDocumentExecutionListener>>())
                {
                    ops.Listeners.Add(listener);
                }

                return ops;
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/__ready", options: new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("ready")
            });

            app.UseHealthChecks("/__live", options: new HealthCheckOptions
            {
                Predicate = _ => false
            });

            app.UseRouting();
            app.UseEndpoints(e => { e.MapDefaultControllerRoute(); });
        }
    }
}

