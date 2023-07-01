using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineStoreApi.Application.Common.Exceptions;
using OnlineStoreApi.Application.Common.Interfaces;
using OnlineStoreApi.Application.Products.DTOs;
using OnlineStoreApi.Domain.Entities;
using OnlineStoreApi.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Commands.UploadFile
{
    public class UploadFileCommand : IRequest<int>
    {


        [Description("Id")]
        public int Id { get; set; }
        public string RootPath { get; set; }
        public IFormFile[] Files { get; set; }
    }

    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UploadFileCommandHandler(
            IApplicationDbContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {

            foreach (IFormFile file in request.Files)
            {
                if (file.Length > 0)
                {
                    string filePath = Path.Combine(request.RootPath, file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }


            var item = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException($"Product with id: [{request.Id}] not found.");
            item.ImagePath = string.Join("^", request.Files.Select(r => r.FileName).ToArray());
            var res = await _context.SaveChangesAsync(cancellationToken);
            return item.Id;
            

        }
    }
}
