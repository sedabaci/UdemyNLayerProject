using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UdemyNLayerProject.API.Extensions;
using UdemyNLayerProject.API.Filters;
using UdemyNLayerProject.Core.Repositories;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Core.UnitOfWorks;
using UdemyNLayerProject.Data;
using UdemyNLayerProject.Data.Repositories;
using UdemyNLayerProject.Data.UnitOfWorks;
using UdemyNLayerProject.Service.Services;

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
        public void ConfigureServices(IServiceCollection services) // Servislerimi eklediğim method.
        {
            /// <summary>
            /// Dependency injecktion
            /// Bir request esnasında classın ctorunda interface ile karsılasırsa, gidip ilgili class'tan nesne örneği al.
            /// eğer birden fazla IUnitOfWork ile karsılasırsa aynı nesne örneği ile devam eder(AddScoped).
            /// eğer AddTransient kullansaydık her seferinde yeni bir nesne örneği alırdı.
            /// </summary>


            services.AddAutoMapper(typeof(Startup));                                       //Entityleri DTO'lara dönüştürür
            services.AddScoped<ProductNotFoundFilter>();                                   //Filter içnerisinde interface tanımlandığı için(DI) önce buraya kaydettik.
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));               //IRepository ile karsılasırsan Repository classından nesne örneği al IRepository'e ata
            services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));    //IService ile karsılasırsan Service classından nesne örneği al IService'e ata
            services.AddScoped<ICategoryService, CategoryServices>();                      //ICategoryService ile karsılasırsan CategoryServices classından nesne örneği al ICategoryService'e ata
            services.AddScoped<IProductService, ProductServices>();                        //IProductService ile karsılasırsan ProductServices classından nesne örneği al IProductService'e ata
            services.AddScoped<IUnitOfWork, UnitOfWork>();                                 //IUnitOfWork ile karsılasırsan UnitOfWork classından nesne örneği al IUnitOfWork'e ata
            services.AddSwaggerDocument(config=> {                                          // Swaggger implemente edildi
                config.PostProcess = (doc =>
                {
                    doc.Info.Title = "First API! | SB";
                    doc.Info.Contact = new NSwag.OpenApiContact()
                    {
                        Name = "Seda Bacı",
                        Email = "sedabacii@gmail.com"
                    };
                });
            });                                                                           

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:SqlConStr"].ToString(), o =>
                {
                    o.MigrationsAssembly("UdemyNLayerProject.Data");
                });
            });

            services.AddControllers(O =>
            {
                O.Filters.Add(new ValidationFilter());  // tüm controllerların içerisinde çalışır
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                //validationlara karısma ben Filter yazıp hallederim diyoruz
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Katmanlarımı eklediğim method.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomException();       // Custom extension method implemente edildi
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseOpenApi();    // Swaggger implemente edildi
            app.UseSwaggerUi3(); // Swaggger implemente edildi

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
