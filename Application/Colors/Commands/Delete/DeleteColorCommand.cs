// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using OnlineStoreApi.Application.Common.Interfaces;
using AutoMapper;
using OnlineStoreApi.Domain.Events;
using OnlineStoreApi.Application.Common.Exceptions;
using OnlineStoreApi.Application.Common.Security;

namespace OnlineStoreApi.Application.Colors.Commands.Delete;

[Authorize]
public class DeleteColorCommand : IRequest<int>
{
      public int Id {  get; }
    }

    public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommand, int>

    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeleteColorCommandHandler(
            IApplicationDbContext context,
             IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Colors.FindAsync(request.Id);
            if(item == null )
            {
                return 0;
            }
            _context.Colors.Remove(item);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result;
        }

    }

