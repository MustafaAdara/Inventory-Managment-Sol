using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MOFA.StockManagement.Api.Services.Controllers.Base;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Infrastructure.Business;
using MOFA.StockManagement.Infrastructure.Models;

namespace MOFA.StockManagement.Api.Services.Controllers
{
    public class ItemTypesController : ControllerBase<IItemTypeService, ItemType, ItemTypeModel, Guid>
    {
        private readonly IItemTypeService _service;
        private readonly IMapper _mapper;
        public ItemTypesController(IItemTypeService service, LinkGenerator linkGenerator, IMapper mapper) : base(service, linkGenerator, mapper)
        {
            _service = service;
            _mapper = mapper;
        }

    }
}
