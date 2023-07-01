using OnlineStoreApi.Application.Common.Exceptions;
using OnlineStoreApi.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using OnlineStoreApi.Application.Products.Commands.Delete;
using OnlineStoreApi.Application.Products.Commands.AddEdit;
using OnlineStoreApi.Application.Colors.Commands.AddEdit;
using OnlineStoreApi.Application.Sizes.Commands.AddEdit;

namespace OnlineStoreApi.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class DeleteProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidProduct()
    {
        var userId = await RunAsDefaultUserAsync();
        var command = new DeleteProductCommand() ;
        command.Id = 99;

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoItem()
    {

        var userId = await RunAsDefaultUserAsync();
        var colorid = await SendAsync(new AddEditColorCommand { Name = "White", ColorCode = "#FFFFFF" });
        //colorid.Should().BePositive();
        var sizeid = await SendAsync(new AddEditSizeCommand { Name = "small" });
        //sizeid.Should().BePositive();
        var itemId = await SendAsync(new AddEditProductCommand
        {
            Name = "New Product 3",
            SizeId = sizeid,
            ColorId = colorid,
            Price = 1,
            PriceType = 1,
            DiscountAmount = 0,
            DiscountExpireAt = null,
            Id = 0
        });

        var command = new DeleteProductCommand();
        command.Id = itemId;
        await SendAsync(command);

        var item = await FindAsync<Product>(itemId);

        item.Should().BeNull();
    }
}
