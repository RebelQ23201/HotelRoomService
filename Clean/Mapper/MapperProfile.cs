using Clean.Model.Output;
using CleanService.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Mapper
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateMap<Company, CompanyOutputModel>()
                .ReverseMap();
            CreateMap<Employee, EmployeeOutputModel>()
                .ReverseMap();
            CreateMap<Hotel, HotelOutputModel>()
                .ReverseMap();
            CreateMap<OrderDetail, OrderDetailOutputModel>()
                            .ReverseMap();
            
            CreateMap<Room, RoomOutputModel>()
                            .ReverseMap();
            CreateMap<RoomOrderOutputModel, RoomOrderOutputModel>()
                            .ReverseMap();
            CreateMap<RoomService, RoomServiceOutputModel>()
                            .ReverseMap();
            CreateMap<RoomType, RoomTypeOutputModel>()
                            .ReverseMap();
            CreateMap<Service, ServiceOutputModel>()
                            .ReverseMap();
            CreateMap<SystemRoomType, SystemRoomTypeOutputModel>()
                            .ReverseMap();
            CreateMap<HotelMember, HotelMemberOutputModel>()
                            .ReverseMap();
            CreateMap<Role, RoleOutputModel>()
                            .ReverseMap();
            CreateMap<Order, OrderOutputModel>()
                .ForMember(
                dest=>dest.RoomOrders,
                opt=>opt.MapFrom(
                    src=> src.RoomOrders.Select(ro=>new RoomOrderOutputModel { 
                        Room = new RoomOutputModel { RoomId = ro.Room.RoomId, HotelId=ro.Room.HotelId, Name=ro.Room.Name, RoomTypeId=ro.Room.RoomTypeId },
                        OrderDetails = ro.OrderDetails.Select(od=>new OrderDetailOutputModel
                        {
                            ServiceId=od.ServiceId,
                            Service=new ServiceOutputModel { CompanyId=od.Service.CompanyId, ServiceId= od.ServiceId.Value,
                                Name= od.Service.Name, Price= od.Service.Price},
                            Quantity=od.Quantity,
                            EmployeeId=od.EmployeeId,
                            Employee=new EmployeeOutputModel { EmployeeId= od.EmployeeId.Value, Name=od.Employee.Name,
                                Phone=od.Employee.Phone, Status=od.Employee.Status, Address=od.Employee.Address},
                            Price=od.Price
                        }).ToList()
                    })
                    )
                )
                .ReverseMap();
            CreateMap<Account, AccountOutputModel>()
                .ForMember(
                    dest => dest.Role,
                    opt => opt.MapFrom(
                        src => new Role
                        {
                            RoleId = src.Role.RoleId,
                            RoleName = src.Role.RoleName
                        }))
                .ReverseMap();
        }
    }
}
