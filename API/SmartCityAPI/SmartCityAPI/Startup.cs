using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartCityAPI.DAO;
using SmartCityAPI.Datas;

namespace SmartCityAPI
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
            services.Configure<DbSettings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoDB:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoDB:Database").Value;
            });
            services.AddControllers();
            services.AddTransient<IServiceContext, ServiceContext>();
            services.AddTransient<IServiceDAO, ServiceDAO>();
            services.AddTransient<IUserContext, UserContext>();
            services.AddTransient<IUserDAO, UserDAO>();
            services.AddTransient<IInterestContext, InterestContext>();
            services.AddTransient<IInterestDAO, InterestDAO>();
            services.AddTransient<INetworkContext, NetworkContext>();
            services.AddTransient<INetworkDAO, NetworkDAO>();
            services.AddTransient<IPublicationContext, PublicationContext>();
            services.AddTransient<IPublicationDAO, PublicationDAO>();
            services.AddTransient<ISubscriptionContext, SubscriptionContext>();
            services.AddTransient<ISubscriptionDAO, SubscriptionDAO>();
            services.AddTransient<ICounterContext, CounterContext>();
            services.AddTransient<ICounterDAO, CounterDAO>();
            services.AddTransient<ITradeTypeContext, TradeTypeContext>();
            services.AddTransient<ITradeTypeDAO, TradeTypeDAO>();
            services.AddTransient<ITradeContext, TradeContext>();
            services.AddTransient<ITradeDAO, TradeDAO>();
            services.AddTransient<IOfferContext, OfferContext>();
            services.AddTransient<IOfferDAO, OfferDAO>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
