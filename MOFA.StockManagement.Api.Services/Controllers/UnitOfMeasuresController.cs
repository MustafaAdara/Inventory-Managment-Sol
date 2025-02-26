using AutoMapper;
using MOFA.StockManagement.Api.Services.Controllers.Base;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Infrastructure.Business;
using MOFA.StockManagement.Infrastructure.Models;

namespace MOFA.StockManagement.Api.Services.Controllers
{
    public class UnitOfMeasuresController : ControllerBase<IUnitOfMeasureService, UnitOfMeasure, UnitOfMeasureModel, Guid>
    {
        private readonly IUnitOfMeasureService _service;
        private readonly IMapper _mapper;
        public UnitOfMeasuresController(IUnitOfMeasureService service, LinkGenerator linkGenerator, IMapper mapper) : base(service, linkGenerator, mapper)
        {
            _service = service;
            _mapper = mapper;
        }

    }
}
