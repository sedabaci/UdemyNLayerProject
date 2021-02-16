using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using UdemyNLayerProject.Web.ApiService;
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
            services.AddHttpClient<CategoryApiService>(opt =>
            {
                opt.BaseAddress = new Uri(Configuration["baseUrl"]);                       //HttpClient nesnesi CategoryApiService ctorunda kullanılabilir, Endpointte baseUrl verdik
            });
            services.AddHttpClient<ProductApiService>(opt =>
            {
                opt.BaseAddress = new Uri(Configuration["baseUrl"]);
            });

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
