using System.Runtime.Serialization;
using AutoMapper;
using OnlineStoreApi.Application.Common.Mappings;
using OnlineStoreApi.Application.Common.Models;
using OnlineStoreApi.Domain.Entities;
using NUnit.Framework;
using OnlineStoreApi.Application.Products.DTOs;
using OnlineStoreApi.Application.Products.Commands.AddEdit;
using OnlineStoreApi.Application.Sizes.DTOs;
using OnlineStoreApi.Application.Colors.DTOs;

namespace OnlineStoreApi.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddProfile<MappingProfile>());

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(Product), typeof(ProductDto))]
    [TestCase(typeof(AddEditProductCommand), typeof(Product))]
    [TestCase(typeof(Product), typeof(LookupDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}
