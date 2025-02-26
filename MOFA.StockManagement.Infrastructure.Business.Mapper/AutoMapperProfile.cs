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

            CreateMap<Item, ItemModel>()
                .ReverseMap();
            CreateMap<ItemModel, ItemModel>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.RowVersion, opts => opts.Ignore());

            CreateMap<Consumer, ConsumerModel>()
                .ReverseMap();
            CreateMap<ConsumerModel, ConsumerModel>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.RowVersion, opts => opts.Ignore());

            CreateMap<Order, OrderModel>()
                .ReverseMap();
            CreateMap<OrderModel, OrderModel>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.RowVersion, opts => opts.Ignore());

            CreateMap<OrderDetail, OrderDetailModel>()
                .ReverseMap();
            CreateMap<OrderDetailModel, OrderDetailModel>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.RowVersion, opts => opts.Ignore());

            CreateMap<OrderSerial, OrderSerialModel>()
                .ReverseMap();
            CreateMap<OrderSerialModel, OrderSerialModel>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.RowVersion, opts => opts.Ignore());
            
            CreateMap<ItemUnitOfMeasure, ItemUnitOfMeasureModel>()
                .ReverseMap();
            CreateMap<ItemUnitOfMeasureModel, ItemUnitOfMeasureModel>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.RowVersion, opts => opts.Ignore());
            
            CreateMap<StockBalance, StockBalanceModel>()
                .ReverseMap();
            CreateMap<StockBalanceModel, StockBalanceModel>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.RowVersion, opts => opts.Ignore());
            
            CreateMap<Supplier, SupplierModel>()
                .ReverseMap();
            CreateMap<SupplierModel, SupplierModel>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.RowVersion, opts => opts.Ignore());
            
            CreateMap<SupplierItem, SupplierItemModel>()
                .ReverseMap();
            CreateMap<SupplierItemModel, SupplierItemModel>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.RowVersion, opts => opts.Ignore());
            
            CreateMap<Transaction, TransactionModel>()
                .ReverseMap();
            CreateMap<TransactionModel, TransactionModel>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.RowVersion, opts => opts.Ignore());

            CreateMap<TransactionDetail, TransactionDetailModel>()
                .ReverseMap();
            CreateMap<TransactionDetailModel, TransactionDetailModel>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.RowVersion, opts => opts.Ignore());
            
            CreateMap<UnitOfMeasure, UnitOfMeasureModel>()
                .ReverseMap();
            CreateMap<UnitOfMeasureModel, UnitOfMeasureModel>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.RowVersion, opts => opts.Ignore());
        }
    }
}
