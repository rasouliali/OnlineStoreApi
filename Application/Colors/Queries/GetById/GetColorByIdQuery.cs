// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using OnlineStoreApi.Application.Colors.DTOs;
using MediatR;
using OnlineStoreApi.Application.Common.Interfaces;
using AutoMapper;
using OnlineStoreApi.Application.Common.Exceptions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineStoreApi.Application.Common.Security;

namespace OnlineStoreApi.Application.Colors.Queries.GetById;
[Authorize]
public class GetColorByIdQuery : IRequest<ColorDto>
{
    public int Id { get; set; }
}

public class GetColorByIdQueryHandler :
     IRequestHandler<GetColorByIdQuery, ColorDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetColorByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ColorDto> Handle(GetColorByIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement GetColorByIdQueryHandler method 
        var data = await _context.Colors.Where(r=>r.Id== request.Id)
                     .ProjectTo<ColorDto>(_mapper.ConfigurationProvider)
                     .FirstAsync(cancellationToken) ?? throw new NotFoundException($"Color with id: [{request.Id}] not found."); ;
        return data;
    }
}


