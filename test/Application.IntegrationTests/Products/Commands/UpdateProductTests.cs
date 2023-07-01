using OnlineStoreApi.Application.Common.Exceptions;
using OnlineStoreApi.Application.Products.Commands.AddEdit;
using OnlineStoreApi.Application.Products.Commands.Delete;
using OnlineStoreApi.Application.Products.Queries.GetAll;
using OnlineStoreApi.Domain.Entities;
using OnlineStoreApi.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;
using OnlineStoreApi.Application.Colors.Commands.AddEdit;
using OnlineStoreApi.Application.Sizes.Commands.AddEdit;

namespace OnlineStoreApi.Application.IntegrationTests.Products.Commands;

using static Testing;

public class UpdateProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidProductId()
    {
        var userId = await RunAsDefaultUserAsync();
        var command = new AddEditProductCommand { Id = 99, Name = "New Product" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateProduct()
    {
        var userId = await RunAsDefaultUserAsync();
        var colorid = await SendAsync(new AddEditColorCommand {Name="White", ColorCode = "#FFFFFF" });
        //colorid.Should().BePositive();
        var sizeid = await SendAsync(new AddEditSizeCommand { Name = "small" });
        //sizeid.Should().BePositive();

        var id0 = await SendAsync(new AddEditProductCommand
        {
            Name = "New Product",
            SizeId = sizeid,
            ColorId = colorid,
            Price = 1,
            PriceType = 1,
            DiscountAmount = 0,
            DiscountExpireAt = null,
            Id = 0
        });


        var command = new AddEditProductCommand
        {
            Name = "New Product2",
            SizeId = sizeid,
            ColorId = colorid,
            Price = 1,
            PriceType = 1,
            DiscountAmount = 0,
            DiscountExpireAt = null,
            Id = id0,
        };

        var productid=await SendAsync(command);
        productid.Should().BePositive();

        var item = await FindAsync<Product>(productid);

        item.Should().NotBeNull();
        item.Name.Should().Be(command.Name);
    }
}
