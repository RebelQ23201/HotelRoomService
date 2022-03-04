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
            CreateMap<CompanyOutputModel, Company>()
                .ReverseMap();
            CreateMap<EmployeeOutputModel, Employee>()
                .ReverseMap();
            CreateMap<HotelOutputModel, Hotel>()
                .ReverseMap();
            CreateMap<OrderDetailOutputModel, OrderDetail>()
                            .ReverseMap();
            CreateMap<OrderOutputModel, Order>()
                            .ReverseMap();
            CreateMap<RoomOutputModel, Room>()
                            .ReverseMap();
            CreateMap<RoomOrderModel, RoomOrder>()
                            .ReverseMap();
            CreateMap<RoomServiceOutputModel, RoomService>()
                            .ReverseMap();
            CreateMap<RoomTypeOutputModel, RoomType>()
                            .ReverseMap();
            CreateMap<ServiceOutputModel, Service>()
                            .ReverseMap();
            CreateMap<SystemRoomTypeOutputModel, SystemRoomType>()
                            .ReverseMap();
            CreateMap<HotelMemberOutputModel, HotelMember>()
                            .ReverseMap();
            CreateMap<RoleOutputModel, Role>()
                            .ReverseMap();
            CreateMap<AccountOutputModel, Account>()
                            .ReverseMap();
        }
    }
}
