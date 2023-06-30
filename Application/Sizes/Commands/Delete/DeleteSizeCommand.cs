// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using OnlineStoreApi.Application.Common.Interfaces;
using AutoMapper;
using OnlineStoreApi.Domain.Events;
using OnlineStoreApi.Application.Common.Exceptions;

namespace OnlineStoreApi.Application.Sizes.Commands.Delete;

    public class DeleteSizeCommand : IRequest<int>
{
      public int Id {  get; }
    }

    public class DeleteSizeCommandHandler : IRequestHandler<DeleteSizeCommand, int>

    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeleteSizeCommandHandler(
            IApplicationDbContext context,
             IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteSizeCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Sizes.FindAsync(request.Id);
            if(item == null )
            {
                return 0;
            }
            _context.Sizes.Remove(item);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result;
        }

    }

