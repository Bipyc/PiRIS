using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bank.DataManagement.Contexts;
using Bank.Models;
using Bank.Services.Implementations;
using Bank.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bank
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
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<CitizenshipContext>(options =>
                options.UseSqlServer(connection));
            services.AddDbContext<CityContext>(options =>
                options.UseSqlServer(connection));

            services.AddDbContext<ClientContext>(options =>
                options.UseSqlServer(connection));
            services.AddDbContext<DisabilityContext>(options =>
                options.UseSqlServer(connection));
            services.AddDbContext<MaritalStatusContext>(options =>
                options.UseSqlServer(connection));

            services.AddDbContext<AccountContext>(options =>
                options.UseSqlServer(connection));
            services.AddDbContext<TransactionContext>(options =>
                options.UseSqlServer(connection));
            services.AddDbContext<TransactionTypeContext>(options =>
                options.UseSqlServer(connection));
            services.AddDbContext<ContractContext>(options =>
                options.UseSqlServer(connection));
            services.AddDbContext<DepositContext>(options =>
                options.UseSqlServer(connection));
            services.AddDbContext<CurrencyTypeContext>(options =>
                options.UseSqlServer(connection));
            services.AddDbContext<YearProcentDepositCurrencyContext>(options =>
                options.UseSqlServer(connection));

            services.AddDbContext<ConfigContext>(options =>
                options.UseSqlServer(connection));

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicy"));
            });

            services.AddSingleton<IAccountHelper, AccountHelper>();
            services.AddSingleton<IAccountNumberGenerator, AccountNumberGenerator>();
            services.AddSingleton<IAccountRegistrator, AccountRegistrator>();
            services.AddSingleton<ITransactionHelper, TransactionHelper>();
            services.AddSingleton<IContractCreator, ContractCreator>();
            services.AddSingleton<IDepositProcessor, DepositProcessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
            app.UseMvc();
        }
    }
}
