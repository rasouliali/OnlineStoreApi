// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using OnlineStoreApi.Application.Sizes.DTOs;
using MediatR;
using AutoMapper;
using OnlineStoreApi.Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace OnlineStoreApi.Application.Sizes.Queries.GetAll;

public class GetAllSizesQuery : IRequest<List<SizeDto>>
{
}

public class GetAllSizesQueryHandler : IRequestHandler<GetAllSizesQuery, List<SizeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllSizesQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SizeDto>> Handle(GetAllSizesQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Sizes
         .OrderBy(x => x.Name)
         .ProjectTo<SizeDto>(_mapper.ConfigurationProvider)
         .ToListAsync();
        return data;
    }
}


