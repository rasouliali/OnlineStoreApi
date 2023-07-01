// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using OnlineStoreApi.Application.Colors.DTOs;
using MediatR;
using AutoMapper;
using OnlineStoreApi.Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineStoreApi.Application.Common.Security;

namespace OnlineStoreApi.Application.Colors.Queries.GetAll;

[Authorize]
public class GetAllColorsQuery : IRequest<List<ColorDto>>
{
}

public class GetAllColorsQueryHandler : IRequestHandler<GetAllColorsQuery, List<ColorDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllColorsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ColorDto>> Handle(GetAllColorsQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Colors
         .OrderBy(x => x.Name)
         .ProjectTo<ColorDto>(_mapper.ConfigurationProvider)
         .ToListAsync();
        return data;
    }
}


