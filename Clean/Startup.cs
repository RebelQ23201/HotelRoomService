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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clean", Version = "v1" });
            });


            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddSingleton(typeof(IBaseService<Account>), typeof(AccountService));
            services.AddSingleton(typeof(IBaseService<Employee>), typeof(EmployeeService));
            services.AddSingleton(typeof(IBaseService<HotelMember>), typeof(HotelMemberService));
            services.AddSingleton(typeof(IBaseService<Hotel>), typeof(HotelService));
            services.AddSingleton(typeof(IBaseService<OrderDetail>), typeof(OrderDetailService));
            services.AddSingleton(typeof(IBaseService<Order>), typeof(OrderService));
            services.AddSingleton(typeof(IBaseService<Role>), typeof(RoleService));
            services.AddSingleton(typeof(IBaseService<Room>), typeof(RoomManagementService));
            services.AddSingleton(typeof(IBaseService<RoomOrder>), typeof(RoomOrderService));
            services.AddSingleton(typeof(IBaseService<RoomService>), typeof(RoomServiceService));
            services.AddSingleton(typeof(IBaseService<RoomType>), typeof(RoomTypeService));
            services.AddSingleton(typeof(IBaseService<Service>), typeof(ServiceService));
            services.AddSingleton(typeof(IBaseService<SystemRoomType>), typeof(SystemRoomTypeService));
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
