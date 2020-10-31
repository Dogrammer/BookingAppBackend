using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingCore.Repository;
using BookingCore.Services;
using BookingDomain;
using BookingDomain.Domain;
using BookingInfrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

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

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddControllers();

            services.AddCors();

            services.AddScoped<DbContext, ApplicationDbContext>();

            //register services
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IApartmentGroupService, ApartmentGroupService>();
            services.AddScoped<IApartmentService, ApartmentService>();
            services.AddScoped<IApartmentTypeService, ApartmentTypeService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IPricingPeriodService, PricingPeriodService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IPricingPeriodDetailService, PricingPeriodDetailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IImageService, ImageService>();



            //services.AddScoped<IUserApartmentGroupService, UserApartmentGroupService>();



            //register repositories
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped(typeof(ITrackableRepository<>), typeof(TrackableRepository<>));
            services.AddTransient<ITrackableRepository<Country>, TrackableRepository<Country>>();
            services.AddTransient<ITrackableRepository<ApartmentGroup>, TrackableRepository<ApartmentGroup>>();
            services.AddTransient<ITrackableRepository<Apartment>, TrackableRepository<Apartment>>();
            services.AddTransient<ITrackableRepository<ApartmentType>, TrackableRepository<ApartmentType>>();
            services.AddTransient<ITrackableRepository<City>, TrackableRepository<City>>();
            services.AddTransient<ITrackableRepository<Location>, TrackableRepository<Location>>();
            services.AddTransient<ITrackableRepository<PricingPeriod>, TrackableRepository<PricingPeriod>>();
            services.AddTransient<ITrackableRepository<Reservation>, TrackableRepository<Reservation>>();
            services.AddTransient<ITrackableRepository<PricingPeriodDetail>, TrackableRepository<PricingPeriodDetail>>();
            services.AddTransient<ITrackableRepository<User>, TrackableRepository<User>>();
            services.AddTransient<ITrackableRepository<Image>, TrackableRepository<Image>>();



            //services.AddTransient<ITrackableRepository<UserApartmentGroup>, TrackableRepository<UserApartmentGroup>>();
            services.AddDistributedMemoryCache();
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
            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequiredLength = 7;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
            });

            builder.AddSignInManager<SignInManager<User>>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = "https://localhost:5001",
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:5001",
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("A-VERY-STRONG-KEY-HERE"))
                };

            });

            services.AddControllers().AddNewtonsoftJson(options =>
     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
 );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            //app.UseSwagger();

            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

            //});

            app.UseRouting();

            
            app.UseCors(
                options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            );

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
