using OnlineStoreApi.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using OnlineStoreApi.Application.IntegrationTests;
using OnlineStoreApi.Application.Products.Queries.GetAll;
using OnlineStoreApi.Application.Products.Commands.AddEdit;

namespace OnlineStoreApi.Application.IntegrationTests.Products.Queries;

using static Testing;

public class GetProductsTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPriceTypes()
    {
        await RunAsDefaultUserAsync();

        var query = new GetAllProductsQuery();

        var result = await SendAsync(query);

        result.PriceTypes.Should().NotBeEmpty();
    }

    [Test]
    public async Task ShouldReturnAllListsAndItems()
    {
        await RunAsDefaultUserAsync();

        await AddAsync(new Product
        {
            Name = "My Product",
            ColorId = 1,
            SizeId = 2,
            Price = 1,
            PriceType = (Domain.Enums.PriceType)1,
            DiscountAmount = 0,
            DiscountExpireAt = null,
            Id = 0

        });

        var query = new GetAllProductsQuery();

        var result = await SendAsync(query);

        result.Datas.Should().HaveCount(1);
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var query = new GetAllProductsQuery();

        var action = () => SendAsync(query);

        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
