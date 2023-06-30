using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.Application.Common.Models;
using OnlineStoreApi.Application.Sizes.Commands.AddEdit;
using OnlineStoreApi.Application.Sizes.Commands.Delete;
using OnlineStoreApi.Application.Sizes.DTOs;
using OnlineStoreApi.Application.Sizes.Queries.GetAll;
using OnlineStoreApi.Application.Sizes.Queries.GetById;
using OnlineStoreApi.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStoreApi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SizesController : ApiControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<List<SizeDto>>> Get()
        {
            var query = new GetAllSizesQuery();
            return await Mediator.Send(query);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SizeDto>> Get(GetSizeByIdQuery query)
        {
            return await Mediator.Send(query);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] AddEditSizeCommand query)
        {
            return await Mediator.Send(query);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] AddEditSizeCommand query)
        {
            return await Mediator.Send(query);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(DeleteSizeCommand query)
        {
            await Mediator.Send(query);
        }
    }
}
