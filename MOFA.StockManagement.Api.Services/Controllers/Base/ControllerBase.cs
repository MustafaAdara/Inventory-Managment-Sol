using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Infrastructure.Business;
using MOFA.StockManagement.Infrastructure.Models;

namespace MOFA.StockManagement.Api.Services.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerBase<TService, TEntity, TModel, TKey> : ControllerBase
        where TService : IServiceBase<TEntity, TModel, TKey>
        where TEntity : EntityBase<TKey>
        where TModel : ModelEntityBase<TKey>
        where TKey : notnull
    {
        private readonly TService _service;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public ControllerBase(TService service, LinkGenerator linkGenerator, IMapper mapper)
        {
            _service = service;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }
        // GET api/<controller>
        [HttpGet]
        public virtual async Task<IActionResult> GetAsync()
        {
            try
            {
                var models = await _service.SelectAsync();
                return Ok(models);
            }
            catch (Exception e)
            {
                var error = e.InnerException != null ? e.InnerException.Message : e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure [{error}]");
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAsync(TKey id)
        {
            try
            {
                var model = await _service.SelectByIdAsync(id);
                return Ok(model);
            }
            catch (Exception e)
            {
                var error = e.InnerException != null ? e.InnerException.Message : e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure [{error}]");
            }
        }

        [HttpGet("Details/{id}")]
        public virtual async Task<IActionResult> GetDetailsAsync(TKey id)
        {
            try
            {
                var model = await _service.DetailsAsync(id);
                return Ok(model);
            }
            catch (Exception e)
            {
                var error = e.InnerException != null ? e.InnerException.Message : e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure [{error}]");
            }
        }

        // GET api/<controller>/page/5
        [HttpGet("page/{page:int}")]
        [HttpGet("page/{page:int}/{pageSize:int}")]
        [HttpGet("page/{page:int}/{sortBy}")]
        [HttpGet("page/{page:int}/{pageSize:int}/{sortBy}")]
        [HttpGet("page/{page:int}/{pageSize:int}/{sortBy}/{sortAsc:bool}")]
        public virtual async Task<IActionResult> GetAsync(int page, string sortBy = "Name", bool sortAsc = true, int? pageSize = 10, string searchBy = "", string type = "")
        {
            try
            {
                PaginationModel<TModel> model;
                if (string.IsNullOrEmpty(searchBy) && string.IsNullOrEmpty(type))
                    model = await _service.SelectPaginationAsync(page, sortBy, pageSize.GetValueOrDefault(10), sortAsc);
                else
                    model = await _service.SelectPaginationSearchAsync(type, searchBy, page, sortBy, pageSize.GetValueOrDefault(10), sortAsc);
                return Ok(model);
            }
            catch (Exception e)
            {
                var error = e.InnerException != null ? e.InnerException.Message : e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure [{error}]");
            }
        }

        // POST api/<controller>
        [HttpPost]
        public virtual async Task<IActionResult> PostAsync(TModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                model.ModifiedBy = User.Identity?.Name;
                var result = await _service.AddAsync(model);

                var controllerName = ControllerContext.RouteData.Values["controller"]?.ToString() ?? "";
                var location = _linkGenerator.GetPathByAction("Get", controllerName, new { id = result.Id });

                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("location");
                }

                return Created(location, result);
            }
            catch (Exception e)
            {
                var error = e.InnerException != null ? e.InnerException.Message : e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure [{error}]");
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> PutAsync(TKey id, TModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modelToUpdate = await _service.SelectByIdAsync(id);
            if (modelToUpdate == null)
            {
                return NotFound();
            }

            try
            {
                _mapper.Map(model, modelToUpdate);
                modelToUpdate.ModifiedBy = User.Identity?.Name;
                var result = await _service.ModifyAsync(modelToUpdate);

                return Ok(result);
            }
            catch (Exception e)
            {
                var error = e.InnerException != null ? e.InnerException.Message : e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure [{error}]");
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(TKey id)
        {
            try
            {
                var result = await _service.RemoveAsync(id);
                if (result)
                {
                    return Ok();
                }

                return BadRequest("Fail to delete");
            }
            catch (Exception e)
            {
                var error = e.InnerException != null ? e.InnerException.Message : e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure [{error}]");
            }
        }
    }
}
