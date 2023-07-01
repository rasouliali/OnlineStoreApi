using Application.Products.Commands.UploadFile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
        float _dollarPrice;
        string _uploadPath;
        public ProductsController(IConfiguration config, IWebHostEnvironment env)
        {
            _uploadPath = Path.Combine(env.ContentRootPath, "wwwroot", "img");
            _dollarPrice = config.GetValue<float>("DollarPriceToman");
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<ProductVm>> Get([FromQuery] GetAllProductsQuery query)
        {
            query.SetDollarPrice(_dollarPrice);
            return await Mediator.Send(query);
        }
        // GET: api/<ValuesController>
        [HttpGet("GetPagination")]
        public async Task<ActionResult<PaginatedList<ProductDto>>> GetPagination([FromQuery] GetProductsWithPaginationQuery query)
        {
            query.SetDollarPrice(_dollarPrice);
            return await Mediator.Send(query);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get([FromQuery] GetProductByIdQuery query)
        {
            query.SetDollarPrice(_dollarPrice);
            return await Mediator.Send(query);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] AddEditProductCommand command)
        {
            return await Mediator.Send(command);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] AddEditProductCommand command)
        {
            return await Mediator.Send(command);
        }

        //POST api/<ValuesController>/5
        [HttpPost("UploadFiles/{id}")]
        public async Task<ActionResult<int>> UploadFiles(int id, IFormFile[] fileUploads)
        {
            UploadFileCommand command = new UploadFileCommand();
            command.Id = id;
            command.Files = fileUploads;
            command.RootPath = _uploadPath;
            return await Mediator.Send(command);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(DeleteProductCommand command)
        {
            await Mediator.Send(command);
        }
    }
}
