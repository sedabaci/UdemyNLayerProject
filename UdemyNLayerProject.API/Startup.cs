using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UdemyNLayerProject.Core.UnitOfWorks;
using UdemyNLayerProject.Data;
using UdemyNLayerProject.Data.UnitOfWorks;

namespace UdemyNLayerProject.API
{
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
            // Servislerimi eklediğim method.
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings: SqlConStr"].ToString());
            });


            /// <summary>
            /// Dependency injecktion
            /// Bir request esnasında classın ctorunda IUnitOfWork ile karsılasırsa, gidip UnitOfWork'ten nesne örneği al. 
            /// eğer birden fazla IUnitOfWork ile karsılasırsa aynı nesne örneği ile devam eder(AddScoped).
            /// eğer AddTransient kullansaydık her seferinde yeni bir nesne örneği alırdı.
            /// </summary>
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Katmanlarımı eklediğim method.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
