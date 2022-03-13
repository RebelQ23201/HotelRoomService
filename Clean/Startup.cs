using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Mapper;
using CleanService.Service;
using CleanService.IService;
using CleanService.DBContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Authentication.OAuth;
using Newtonsoft.Json;

namespace Clean
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
            services.AddControllers();
            //.AddJsonOptions(o=>o.JsonSerializerSettings.);

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
            }));

            var key = "This is my first Test Key";
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clean", Version = "v1" });
            });

            IConfiguration config = new ConfigurationBuilder()
                                   .SetBasePath(Directory.GetCurrentDirectory())
                                   .AddJsonFile("appsettings.Development.json",
                                            optional: true,
                                            reloadOnChange: false)
                                   .Build();
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = config["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = config["Authentication:Google:ClientSecret"];
            });

            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddSingleton(typeof(ICompanyService<Company>), typeof(CompanyService));
            services.AddSingleton(typeof(IAccountService<Account>), typeof(AccountService));
            services.AddSingleton(typeof(IEmployeeService<Employee>), typeof(EmployeeService));
            services.AddSingleton(typeof(IBaseService<HotelMember>), typeof(HotelMemberService));
            services.AddSingleton(typeof(IHotelService<Hotel>), typeof(HotelService));
            services.AddSingleton(typeof(IBaseService<OrderDetail>), typeof(OrderDetailService));
            services.AddSingleton(typeof(IBaseService<Order>), typeof(OrderService));
            services.AddSingleton(typeof(IBaseService<Role>), typeof(RoleService));
            services.AddSingleton(typeof(IRoomService<Room>), typeof(RoomManagementService));
            services.AddSingleton(typeof(IBaseService<RoomOrder>), typeof(RoomOrderService));
            services.AddSingleton(typeof(IBaseService<RoomService>), typeof(RoomServiceService));
            services.AddSingleton(typeof(IRoomTypeService<RoomType>), typeof(RoomTypeService));
            services.AddSingleton(typeof(IServiceService<Service>), typeof(ServiceService));
            services.AddSingleton(typeof(ISystemRoomTypeService<SystemRoomType>), typeof(SystemRoomTypeService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean v1"));
            }
            if (env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean v1"));
            }

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "")),
                RequestPath = "/Clean",
                EnableDefaultFiles = true
            });

            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
