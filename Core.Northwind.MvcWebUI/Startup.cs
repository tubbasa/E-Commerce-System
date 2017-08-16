using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Core.Northwind.Business.Abstract;
using Core.Northwind.Business.Concrete;
using Core.Northwind.DataAccess.Abstract;
using Core.Northwind.DataAccess.Concrete.EntityFramework;
using Core.Northwind.MvcWebUI.Middlewares;
using Core.Northwind.MvcWebUI.Services;
using Core.Northwind.MvcWebUI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;

namespace Core.Northwind.MvcWebUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //servis yapılandırmalarını yapıyoruz burada.
            //dependency injection burada gerçekleştirilir!
            services.AddScoped<IProductService, ProductManager>();
            //addsingleton olarak tanımlasaydık eğer biri için productmanager nesnesş oluştuğunda diğeri içinde oluşurdu ortak kullanım olurdu?
            //addscoped : bir kullanıcı pm e istekte bulunduğunda onun için bir pm oluşur a kullanıcısı ikinci bir isteği yaptığında ikinci bir pm oluşur b kullanıcısı da bir istek yaptığında bir pm daha olulur her istek için yeni bir nesne olşur.a kullanıcı aynı anda iki tane pm e ihtiyaç duyar ise onun için aynı pm oluşur ve o verilir
            //addtransient ise aynı anda iki tane istek oluşursa onun için iki tane aynı pm oluşur
            services.AddScoped<IProductDal, EfProductDal>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal,EfCategoryDal>();
            services.AddSingleton<ICartSessionService, CartSessionService>();
            services.AddSingleton<ICartService, CartManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<CustomIdentityDbContext>(options => options.UseSqlServer(@"Server=DESKTOP-3R792NV\SQLEXPRESS;Database=NORTHWND;Trusted_Connection=true"));
            services.AddIdentity<CustomIdentityUser, CustomIdentityRole>().AddEntityFrameworkStores<CustomIdentityDbContext>().AddDefaultTokenProviders();
            services.AddSession();
            services.AddDistributedMemoryCache(); //bu sessionı aktifleştirir


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //orta katman yapılandırılması gerçekleştirilir.
            //bir uygulamanın çalışması sırasında arka arkaya yapılan olaylar isteğin çalıştırılması authenticationdan geçirilmesi vs. .net core sen kendin yazıyorsun bunu.
            //loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseStaticFiles();
            //wwwrootun altını tutar buraya kendi yolumuzu yazmamız gerek 
            app.UseFileServer();
            //hem wwwrootun hem de bizim dışarıdaki css dosyalarımızın yerini gösterir
            app.UseNodeModules(env.ContentRootPath);
            //kendi custom midlewearı nasıl yazarız?
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
          
            app.UseSession();
            app.UseIdentity();
            app.UseMvc(ConfigureRoutes);
            //default olarak home controllerın ındex sayfasına git demektir bu. 
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            //home/ındex/1 isteginde bulunduğunda controllerı home actionım ındex demek oluyor
            routeBuilder.MapRoute("Default","{controller=Product}/{action=Index}/{id?}");
        }
    }
}
