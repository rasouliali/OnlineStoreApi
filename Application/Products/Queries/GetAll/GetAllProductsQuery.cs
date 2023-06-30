﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using OnlineStoreApi.Application.Products.DTOs;
using MediatR;
using OnlineStoreApi.Application.Common.Models;
using AutoMapper;
using OnlineStoreApi.Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineStoreApi.Domain.Entities;
using OnlineStoreApi.Domain.Enums;
using Application.Products.DTOs;

namespace OnlineStoreApi.Application.Products.Queries.GetAll;

public class GetAllProductsQuery : IRequest<ProductVm>
{
}

public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetAllProductsQuery, ProductVm>
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

    public async Task<ProductVm> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        List<ProductDto> data = await _context.Products
         .OrderBy(x => x.Name)
         .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
         .ToListAsync();

        return new ProductVm
        {
            PriceTypes = Enum.GetValues(typeof(PriceType))
                .Cast<PriceType>()
                .Select(p => new PriceTypeDto { Value = (int)p, Name = p.ToString() })
                .ToList(),
            Datas = data
        };
    }
}

