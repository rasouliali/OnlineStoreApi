using OnlineStoreApi.Application.Common.Exceptions;
using OnlineStoreApi.Application.Products.Commands.AddEdit;
using OnlineStoreApi.Application.Products.Commands.Delete;
using OnlineStoreApi.Application.Products.Queries.GetAll;
using OnlineStoreApi.Domain.Entities;
using OnlineStoreApi.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace OnlineStoreApi.Application.IntegrationTests.Products.Commands;

using static Testing;

public class UpdateProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidProductId()
    {
        var command = new AddEditProductCommand { Id = 99, Name = "New Product" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateProduct()
    {
        var userId = await RunAsDefaultUserAsync();

        var id0 = await SendAsync(new AddEditProductCommand
        {
            Name = "New Product",
            SizeId = 2,
            ColorId = 1,
            Price = 1,
            PriceType = 1,
            DiscountAmount = 0,
            DiscountExpireAt = null,
            Id = 0
        });


        var command = new AddEditProductCommand
        {
            Name = "New Product2",
            SizeId = 2,
            ColorId = 1,
            Price = 1,
            PriceType = 1,
            DiscountAmount = 0,
            DiscountExpireAt = null,
            Id = id0,
        };

        await SendAsync(command);

        var item = await FindAsync<Product>(id0);

        item.Should().NotBeNull();
        item.Name.Should().Be(command.Name);
    }
}
