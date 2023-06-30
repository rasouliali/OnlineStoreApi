using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.Application.Common.Models;
using OnlineStoreApi.Application.Products.Commands.AddEdit;
using OnlineStoreApi.Application.Products.Commands.Delete;
using OnlineStoreApi.Application.Products.DTOs;
using OnlineStoreApi.Application.Products.Queries.GetAll;
using OnlineStoreApi.Application.Products.Queries.GetById;
using OnlineStoreApi.Application.Products.Queries.Pagination;
using OnlineStoreApi.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStoreApi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductsController : ApiControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<ProductVm>> Get()
        {
            var query = new GetAllProductsQuery();
            return await Mediator.Send(query);
        }
        // GET: api/<ValuesController>
        [HttpGet("GetPagination")]
        public async Task<ActionResult<PaginatedList<ProductDto>>> GetPagination(GetProductsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(GetProductByIdQuery query)
        {
            return await Mediator.Send(query);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] AddEditProductCommand query)
        {
            return await Mediator.Send(query);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] AddEditProductCommand query)
        {
            return await Mediator.Send(query);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(DeleteProductCommand query)
        {
            await Mediator.Send(query);
        }
    }
}
