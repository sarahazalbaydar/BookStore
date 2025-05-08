using BookStore.Api.DBOperations;
using BookStore.Api.Extensions;
using BookStore.Api.Mapping;
using BookStore.Api.Services.Implementations;
using BookStore.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddSwaggerGen();

        // BookStoreDB injected because database is a service
        services.AddDbContext<BookStoreDbContext>(options =>
            options.UseInMemoryDatabase("BookStoreDB"));

        // Dependency Injection
        services.AddScoped<IBookService, EfBookService>();
        services.AddAutoMapper(typeof(MappingProfile));


    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        // Custom Middleware
        app.UseSimpleLogging();    //Logging with custom middleware
        app.UseCustomSwagger();    //Custom extension method for Swagger

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
