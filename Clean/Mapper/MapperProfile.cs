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
            CreateMap<Order, OrderOutputModel>()
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
