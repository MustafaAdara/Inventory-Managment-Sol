using AutoMapper;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Infrastructure.Models;

namespace MOFA.StockManagement.Infrastructure.Business.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Warehouse, WarehouseModel>()
                .ReverseMap();
            CreateMap<WarehouseModel, WarehouseModel>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.RowVersion, opts => opts.Ignore());

            CreateMap<ItemType, ItemTypeModel>()
                .ReverseMap();
            CreateMap<ItemTypeModel, ItemTypeModel>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.RowVersion, opts => opts.Ignore());

        }
    }
}
