using OnlineStoreApi.Application.Common.Exceptions;
using OnlineStoreApi.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using OnlineStoreApi.Application.Products.Commands.AddEdit;

namespace OnlineStoreApi.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class AddProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new AddEditProductCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
    [Test]
    public async Task ShouldBeGreaterThanZero()
    {
        var command = new AddEditProductCommand
        {
            Name = "prod",
            SizeId = 2,
            ColorId = 1,
            Price = 15000,
            PriceType = 0,
            DiscountAmount = 0,
            DiscountExpireAt = null,
            Id=0
        };
        var res = await SendAsync(command);
        res.Should().Match(x=> x > 0);
    }

}
