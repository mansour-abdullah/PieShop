using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PieShop.Models;

namespace PieShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
         
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            
            services.AddTransient<ICategoryRepository , CategoryRepository>();
            
            services.AddTransient<IPieRepository , PieRepository>();
            
            services.AddTransient<IOrderRepository , OrderRepository>();
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ShoppingCart>(ShoppingCart.GetCart);
            
            services.AddMvc();
            
            services.AddMemoryCache();
            
            services.AddSession();
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
         
        if (!env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
            
            app.UseStatusCodePages();
           
            app.UseStaticFiles();
            
            app.UseSession();

            app.UseIdentity();
            
         
            //app.UseMvcWithDefaultRoute();
          
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name:"categoryfilter",
                    template:"Pie/{action}/{category?}",
                    defaults:new {Controller = "Pie" , action="List"});
                
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
            
            DbInitializer.Seed(app);
                 
        }
    }
}
