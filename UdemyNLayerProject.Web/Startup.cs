using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UdemyNLayerProject.Core.Repositories;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Core.UnitOfWorks;
using UdemyNLayerProject.Data;
using UdemyNLayerProject.Data.Repositories;
using UdemyNLayerProject.Data.UnitOfWorks;
using UdemyNLayerProject.Service.Services;
using UdemyNLayerProject.Web.Filters;

namespace UdemyNLayerProject.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(ProductNotFoundFilter));                             //Filter içnerisinde interface tanımlandığı için(DI) önce buraya kaydettik.
            services.AddScoped(typeof(CategoryNotFoundFilter));                            //Filter içnerisinde interface tanımlandığı için(DI) önce buraya kaydettik.
            services.AddAutoMapper(typeof(Startup));                                       //Entityleri DTO'lara dönüştürür
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));               //IRepository ile karsılasırsan Repository classından nesne örneği al IRepository'e ata
            services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));    //IService ile karsılasırsan Service classından nesne örneği al IService'e ata
            services.AddScoped<ICategoryService, CategoryServices>();                      //ICategoryService ile karsılasırsan CategoryServices classından nesne örneği al ICategoryService'e ata
            services.AddScoped<IProductService, ProductServices>();                        //IProductService ile karsılasırsan ProductServices classından nesne örneği al IProductService'e ata
            services.AddScoped<IUnitOfWork, UnitOfWork>();                                 //IUnitOfWork ile karsılasırsan UnitOfWork classından nesne örneği al IUnitOfWork'e ata
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:SqlConStr"].ToString(), o =>
                {
                    o.MigrationsAssembly("UdemyNLayerProject.Data");
                });
            });

            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
