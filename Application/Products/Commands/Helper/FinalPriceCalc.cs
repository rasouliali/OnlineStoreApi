using MediatR;
using OnlineStoreApi.Application.Products.DTOs;
using OnlineStoreApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Commands.Helper
{
    public class FinalPriceCalc
    {
        public static void Do(object data, float dollarPrice)
        {
            Do((ProductDto)data, dollarPrice);
        }
        public static void Do(ProductDto data,float dollarPrice)
        {
            if (data.PriceType == PriceType.CONSTANT)
                data.FinalPrice = data.Price;
            else
                data.FinalPrice = data.Price * dollarPrice;
            if (data.DiscountExpireAt == null || DateTime.Now <= data.DiscountExpireAt)
                if (data.DiscountAmount != null)
                    data.FinalPrice -= (float)data.DiscountAmount;
        }
    }
}
