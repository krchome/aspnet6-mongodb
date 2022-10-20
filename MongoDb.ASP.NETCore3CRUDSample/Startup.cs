using MongoDb.ASP.NET6CRUDSample.DataAccess;
using MongoDb.ASP.NET6CRUDSample.DataAccess.Repositories;

namespace MongoDb.ASP.NET6CRUDSample
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
            services.AddMvc();
            services.Configure<MongoDb.ASP.NET6CRUDSample.DataAccess.Settings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoDB:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoDB:Database").Value;


            } );
            services.AddTransient<ICustomerContext, CustomerContext>();
            services.AddTransient<ICustomerRepository,CustomerRepository>();
           // services.AddDbContext<MongoDbContext>(opt=>opt.)
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
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
