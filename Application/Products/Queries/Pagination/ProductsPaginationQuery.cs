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
using OnlineStoreApi.Domain.Enums;
using Application.Products.Commands.Helper;

namespace OnlineStoreApi.Application.Products.Queries.Pagination;

public class GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
{
    public string? Keyword { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10; 

    private float _dollarPrice;
    public void SetDollarPrice(float dollarPrice)
    {
        _dollarPrice = dollarPrice;
    }
    public float GetDollarPrice()
    {
        return _dollarPrice;
    }
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
        var data = await _context.Products.Where(x => string.IsNullOrEmpty(request.Keyword)==true || x.Name.Contains(request.Keyword))
         .OrderBy(x => x.Name)
         .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
         .PaginatedListAsync(request.PageNumber, request.PageSize);
        data.Items.ToList().ForEach(r =>
        {
            FinalPriceCalc.Do(r, request.GetDollarPrice());
            r.Images = (r.ImagePath??"").Split("^").Select(s=> "/img/" +s).ToList();
        }); 

        return data;
    }
}
