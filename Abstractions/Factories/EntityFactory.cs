using ConsoleApplication.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApplication.Abstractions.Factories;

public class EntityFactory<TEntity, TBuilder> : IEntityFactory<TEntity, TBuilder>
    where TEntity : class, new()
    where TBuilder : IAbstractBuilder<TEntity>
{
    private readonly IServiceProvider _services;

    public EntityFactory(IServiceProvider services)
    {
        _services = services;
    }

    public TBuilder New()
    {
        return _services.GetService<TBuilder>() ?? ActivatorUtilities.CreateInstance<TBuilder>(_services);
    }

    public TBuilder Edit(TEntity instance)
    {
        var builder = New();
        builder.Edit(instance);

        return builder;
    }
}