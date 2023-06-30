using System.Reflection;
using AutoMapper;
using AutoMapper.Internal;
using FluentValidation;
using MediatR;
using OnlineStoreApi.Application.Common.Behaviours;
using OnlineStoreApi.Application.Common.Mappings;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.Internal().MethodMappingEnabled = false;
            mc.AddProfile(new MappingProfile());
        });

        services.AddAutoMapper(cfg => cfg.Internal().MethodMappingEnabled = false, typeof(MappingProfile).Assembly);

        //services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        });

        return services;
    }
}
