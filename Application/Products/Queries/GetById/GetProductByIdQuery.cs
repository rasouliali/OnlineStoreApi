// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using OnlineStoreApi.Application.Products.DTOs;
using MediatR;
using OnlineStoreApi.Application.Common.Interfaces;
using AutoMapper;
using OnlineStoreApi.Application.Common.Exceptions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineStoreApi.Domain.Enums;
using Application.Products.Commands.Helper;
using OnlineStoreApi.Application.Common.Security;

namespace OnlineStoreApi.Application.Products.Queries.GetById;
[Authorize]
public class GetProductByIdQuery : IRequest<ProductDto>
{

    public int Id { get; set; }
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

public class GetProductByIdQueryHandler :
     IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement GetProductByIdQueryHandler method 
        var data = await _context.Products.Where(r => r.Id == request.Id)
                     .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                     .FirstAsync(cancellationToken) ?? throw new NotFoundException($"Product with id: [{request.Id}] not found."); ;

        FinalPriceCalc.Do(data, request.GetDollarPrice());
        data.Images = (data.ImagePath??"").Split("^").Select(s => "/img/" + s).ToList();


        return data;
    }
}


