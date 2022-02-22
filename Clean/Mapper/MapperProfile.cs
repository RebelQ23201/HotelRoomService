using Clean.Model;
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
            CreateMap<CompanyModel, Company>()
                .ReverseMap();
            CreateMap<EmployeeModel, Employee>()
                .ReverseMap();
            CreateMap<HotelModel, Hotel>()
                .ReverseMap();
            CreateMap<OrderDetailModel, OrderDetail>()
                            .ReverseMap();
            CreateMap<OrderModel, Order>()
                            .ReverseMap();
            CreateMap<RoomModel, Room>()
                            .ReverseMap();
            CreateMap<RoomOrderModel, RoomOrder>()
                            .ReverseMap();
            CreateMap<RoomServiceModel, RoomService>()
                            .ReverseMap();
            CreateMap<RoomTypeModel, RoomType>()
                            .ReverseMap();
            CreateMap<ServiceModel, Service>()
                            .ReverseMap();
            CreateMap<SystemRoomTypeModel, SystemRoomType>()
                            .ReverseMap();
            CreateMap<HotelMemberModel, HotelMember>()
                            .ReverseMap();
            CreateMap<RoleModel, Role>()
                            .ReverseMap();
            CreateMap<AccountModel, Account>()
                            .ReverseMap();
        }
    }
}
