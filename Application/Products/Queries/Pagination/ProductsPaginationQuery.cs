// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using OnlineStoreApi.Application.Products.DTOs;
using System.ComponentModel;
using MediatR;
using OnlineStoreApi.Application.Common.Models;
using OnlineStoreApi.Domain.Entities;
using OnlineStoreApi.Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnlineStoreApi.Application.Common.Mappings;

namespace OnlineStoreApi.Application.Products.Queries.Pagination;

public class GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
{
    public string? Keyword { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
    
public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductDto>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProductDto>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
        {
           // TODO: Implement ProductsWithPaginationQueryHandler method 
           var data = await _context.Products.Where(x=>x.Name.Contains(request.Keyword))
            .OrderBy(x => x.Name)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        return data;
        }
}
