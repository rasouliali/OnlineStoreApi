// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using OnlineStoreApi.Application.Sizes.DTOs;
using OnlineStoreApi.Application.Common.Mappings;
using OnlineStoreApi.Domain.Events;
using OnlineStoreApi.Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using OnlineStoreApi.Domain.Entities;
using OnlineStoreApi.Application.Common.Exceptions;
using OnlineStoreApi.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace OnlineStoreApi.Application.Sizes.Commands.AddEdit;

public class AddEditSizeCommand : IRequest<int>, IMapFrom<Size>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Size, SizeDto>().ReverseMap();
    }

    [Description("Id")]
    public int Id { get; set; }
    [Description("Name")]
    public string Name { get; set; } = String.Empty;
}

    public class AddEditSizeCommandHandler : IRequestHandler<AddEditSizeCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AddEditSizeCommandHandler(
            IApplicationDbContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddEditSizeCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement AddEditSizeCommandHandler method 
            var dto = _mapper.Map<SizeDto>(request);
            if (request.Id > 0)//edit
            {
                var item = await _context.Sizes.FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException($"Size with id: [{request.Id}] not found.");
                item = _mapper.Map(dto, item);
                var res = await _context.SaveChangesAsync(cancellationToken);
                return item.Id;
            }
            else
            {
                var item = _mapper.Map<Size>(dto);
                _context.Sizes.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return item.Id;
            }
           
        }
    }

