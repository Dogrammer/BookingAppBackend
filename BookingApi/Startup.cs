using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingCore.Repository;
using BookingCore.Services;
using BookingDomain;
using BookingDomain.Domain;
using BookingInfrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookingApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddControllers();

            services.AddCors();

            services.AddScoped<DbContext, ApplicationDbContext>();

            //register services
            services.AddScoped<ICountryService, CountryService>();


            //register repositories
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped(typeof(ITrackableRepository<>), typeof(TrackableRepository<>));
            services.AddTransient<ITrackableRepository<Country>, TrackableRepository<Country>>();


            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(new[] {
                    "BookingCore"
                });
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(typeof(Startup));

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            //});

            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    opt => opt.MigrationsAssembly("BookingInfrastructure"))
               );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //app.UseSwagger();

            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

            //});

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
