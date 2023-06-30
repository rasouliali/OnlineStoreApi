using OnlineStoreApi.WebAPI.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStoreApi.WebAPI.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
