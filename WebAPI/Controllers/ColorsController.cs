using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.Application.Common.Models;
using OnlineStoreApi.Application.Colors.Commands.AddEdit;
using OnlineStoreApi.Application.Colors.Commands.Delete;
using OnlineStoreApi.Application.Colors.DTOs;
using OnlineStoreApi.Application.Colors.Queries.GetAll;
using OnlineStoreApi.Application.Colors.Queries.GetById;
using OnlineStoreApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStoreApi.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ApiControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<List<ColorDto>>> Get([FromQuery] GetAllColorsQuery query)
        {
            return await Mediator.Send(query);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ColorDto>> Get(GetColorByIdQuery query)
        {
            return await Mediator.Send(query);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] AddEditColorCommand query)
        {
            return await Mediator.Send(query);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] AddEditColorCommand query)
        {
            return await Mediator.Send(query);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(DeleteColorCommand query)
        {
            await Mediator.Send(query);
        }
    }
}
