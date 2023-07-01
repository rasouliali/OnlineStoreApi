// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using OnlineStoreApi.Domain.Entities;
using OnlineStoreApi.Application.Common.Mappings;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using OnlineStoreApi.Domain.Enums;
using System.Reflection.Metadata.Ecma335;

namespace OnlineStoreApi.Application.Products.DTOs;


public class ProductDto:IMapFrom<Product>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductDto>().ReverseMap();
        
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
    public PriceType PriceType { get; set; }
    [Description("Price")]
    public float Price { get; set; }
    public float FinalPrice { get; set; }
    [Description("List Of Images")]
    public List<string> Images { get; set; }
    public string ImagePath { get; set; }
    [Description("Discount Amount")]
    public float? DiscountAmount { get; set; }
    [Description("Discount Expire At")]
    public DateTime? DiscountExpireAt { get; set; }

}

