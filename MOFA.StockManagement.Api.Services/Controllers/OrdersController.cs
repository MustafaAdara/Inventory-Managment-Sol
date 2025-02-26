using AutoMapper;
using MOFA.StockManagement.Api.Services.Controllers.Base;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Infrastructure.Business;
using MOFA.StockManagement.Infrastructure.Models;

namespace MOFA.StockManagement.Api.Services.Controllers
{
    public class OrdersController : ControllerBase<IOrderService, Order, OrderModel, Guid>
    {
        private readonly IOrderService _service;
        private readonly IMapper _mapper;
        public OrdersController(IOrderService service, LinkGenerator linkGenerator, IMapper mapper) : base(service, linkGenerator, mapper)
        {
            _service = service;
            _mapper = mapper;
        }

    }
}
