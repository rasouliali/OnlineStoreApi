using OnlineStoreApi.Domain.Entities;
using OnlineStoreApi.Domain.Common;
using OnlineStoreApi.Domain.Enums;

namespace OnlineStoreApi.Domain.Entities
{
    public class Product: BaseAuditableEntity
    {
        public string Name { get; set; }
        public string? ImagePath { get; set; }
        public float Price { get; set; }
        public PriceType PriceType { get; set; }
        public float? DiscountAmount { get; set; }
        public DateTime? DiscountExpireAt { get; set; }
        public int SizeId { get; set; }
        public Size CurrentSize { get; set; }
        public int ColorId { get; set; }
        public Color CurrentColor { get; set; }
    }
}