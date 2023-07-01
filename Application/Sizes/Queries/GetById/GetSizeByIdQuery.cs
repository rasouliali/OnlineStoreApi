// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using OnlineStoreApi.Application.Sizes.DTOs;
using MediatR;
using OnlineStoreApi.Application.Common.Interfaces;
using AutoMapper;
using OnlineStoreApi.Application.Common.Exceptions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineStoreApi.Application.Common.Security;

namespace OnlineStoreApi.Application.Sizes.Queries.GetById;
[Authorize]
public class GetSizeByIdQuery : IRequest<SizeDto>
{
    public int Id { get; set; }
}

public class GetSizeByIdQueryHandler :
     IRequestHandler<GetSizeByIdQuery, SizeDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSizeByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SizeDto> Handle(GetSizeByIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement GetSizeByIdQueryHandler method 
        var data = await _context.Sizes.Where(r=>r.Id== request.Id)
                     .ProjectTo<SizeDto>(_mapper.ConfigurationProvider)
                     .FirstAsync(cancellationToken) ?? throw new NotFoundException($"Size with id: [{request.Id}] not found."); ;
        return data;
    }
}


