using OnlineStoreApi.Application.Common.Interfaces;

namespace OnlineStoreApi.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
