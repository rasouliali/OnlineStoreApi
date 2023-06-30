// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using OnlineStoreApi.Application.Products.DTOs;
using OnlineStoreApi.Application.Common.Mappings;
using OnlineStoreApi.Domain.Events;
using OnlineStoreApi.Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using OnlineStoreApi.Domain.Entities;
using OnlineStoreApi.Application.Common.Exceptions;
using OnlineStoreApi.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace OnlineStoreApi.Application.Products.Commands.AddEdit;

public class AddEditProductCommand : IRequest<int>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Name")]
    public string Name { get; set; } = String.Empty;
    [Description("Size")]
    public int SizeId { get; set; }
    [Description("Color Id")]
    public int ColorId { get; set; }
    [Description("Price Type (0=Constant,1=Formula)")]
    public string? PriceType { get; set; }
    [Description("Price")]
    public float Price { get; set; }
    [Description("List Of Images")]
    public List<IFormFile> Images { get; set; }
    [Description("Discount Amount")]
    public float? DiscountAmount { get; set; }
    [Description("Discount Expire At")]
    public DateTime? DiscountExpireAt { get; set; }
}

    public class AddEditProductCommandHandler : IRequestHandler<AddEditProductCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AddEditProductCommandHandler(
            IApplicationDbContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddEditProductCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement AddEditProductCommandHandler method 
            var dto = _mapper.Map<ProductDto>(request);
            if (request.Id > 0)//edit
            {
                var item = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException($"Product with id: [{request.Id}] not found.");
                item = _mapper.Map(dto, item);
				// raise a update domain event
				item.AddDomainEvent(new ProductUpdatedEvent(item));
                var res = await _context.SaveChangesAsync(cancellationToken);
                return item.Id;
            }
            else
            {
                var item = _mapper.Map<Product>(dto);
                // raise a create domain event
				item.AddDomainEvent(new ProductCreatedEvent(item));
                _context.Products.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return item.Id;
            }
           
        }
    }

