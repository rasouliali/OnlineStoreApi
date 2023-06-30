// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using OnlineStoreApi.Domain.Entities;
using OnlineStoreApi.Application.Common.Mappings;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace OnlineStoreApi.Application.Sizes.DTOs;


public class SizeDto : IMapFrom<Size>
{
    public void Mapping(Profile profile)
    {
        //profile.CreateMap<Size, SizeDto>().ReverseMap();
    }

    [Description("Id")]
    public int Id { get; set; }
    [Description("Name")]
    public string Name { get; set; } = String.Empty;

}

