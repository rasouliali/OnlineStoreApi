// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using OnlineStoreApi.Domain.Entities;
using OnlineStoreApi.Application.Common.Mappings;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace OnlineStoreApi.Application.Products.DTOs;


public class ProductDto:IMapFrom<Product>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Size, ProductDto>().ReverseMap();
    }

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
    public List<string> ImageList { get; set; }
    [Description("Discount Amount")]
    public float? DiscountAmount { get; set; }
    [Description("Discount Expire At")]
    public DateTime? DiscountExpireAt { get; set; }

}

