using OnlineStoreApi.Application.Products.DTOs;
using OnlineStoreApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreApi.Application.Products.DTOs
{
    public class ProductVm
    {
        public List<ProductDto> Datas { get; set; }
        public List<PriceTypeDto> PriceTypes { get; set; }
    }
}
