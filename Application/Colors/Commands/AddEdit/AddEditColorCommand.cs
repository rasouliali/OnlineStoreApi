﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using OnlineStoreApi.Application.Colors.DTOs;
using OnlineStoreApi.Application.Common.Mappings;
using OnlineStoreApi.Domain.Events;
using OnlineStoreApi.Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using OnlineStoreApi.Domain.Entities;
using OnlineStoreApi.Application.Common.Exceptions;
using OnlineStoreApi.Domain.Enums;
using Microsoft.AspNetCore.Http;
using OnlineStoreApi.Application.Common.Security;

namespace OnlineStoreApi.Application.Colors.Commands.AddEdit;

[Authorize]
public class AddEditColorCommand : IRequest<int>, IMapFrom<Color>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Color, AddEditColorCommand>().ReverseMap();
    }
    [Description("Id")]
    public int Id { get; set; }
    [Description("Name")]
    public string Name { get; set; } = String.Empty;
    [Description("Color Code")]
    public string ColorCode { get; set; } = String.Empty;
}

    public class AddEditColorCommandHandler : IRequestHandler<AddEditColorCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AddEditColorCommandHandler(
            IApplicationDbContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddEditColorCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement AddEditColorCommandHandler method 
            if (request.Id > 0)//edit
            {
                var item = await _context.Colors.FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException($"Color with id: [{request.Id}] not found.");
                item = _mapper.Map(request, item);
                var res = await _context.SaveChangesAsync(cancellationToken);
                return item.Id;
            }
            else
            {
                var item = _mapper.Map<Color>(request);
                _context.Colors.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return item.Id;
            }
           
        }
    }

