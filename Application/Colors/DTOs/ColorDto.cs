// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using OnlineStoreApi.Domain.Entities;
using OnlineStoreApi.Application.Common.Mappings;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace OnlineStoreApi.Application.Colors.DTOs;


public class ColorDto : IMapFrom<Color>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Color, ColorDto>().ReverseMap();
    }

    [Description("Id")]
    public int Id { get; set; }
    [Description("Name")]
    public string Name { get; set; } = String.Empty;
    [Description("Color Code")]
    public string ColorCode { get; set; } = String.Empty;


}

