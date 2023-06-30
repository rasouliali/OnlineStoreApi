// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using OnlineStoreApi.Application.Common.Interfaces;
using AutoMapper;
using OnlineStoreApi.Domain.Events;

namespace OnlineStoreApi.Application.Products.Commands.Delete;

    public class DeleteProductCommand : IRequest<int>
{
      public int Id {  get; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>

    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeleteProductCommandHandler(
            IApplicationDbContext context,
             IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Products.FindAsync(request.Id);
            if(item == null )
            {
                return 0;
            }
			item.AddDomainEvent(new ProductDeletedEvent(item));
            _context.Products.Remove(item);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result;
        }

    }

