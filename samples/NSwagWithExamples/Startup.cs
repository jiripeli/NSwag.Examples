using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag.Examples;
using NSwagWithExamples.Models.Examples;
using RandomNameGeneratorLibrary;

namespace NSwagWithExamples;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
        services.AddSingleton<IPersonNameGenerator, PersonNameGenerator>();
        services.AddControllers();

        services.AddExampleProviders(typeof(BrnoExample).Assembly);
        services.AddOpenApiDocument(
            (settings, provider) =>
            {
                settings.Title = "NSwag with examples";
                settings.AddExamples(provider);
            });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();
        app.UseOpenApi();
        app.UseSwaggerUi();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}