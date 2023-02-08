using ConsoleApplication.Abstractions.Builders;

namespace ConsoleApplication.Abstractions.Factories;

public interface IEntityFactory<in TEntity, out TBuilder>
    where TEntity : class, new()
    where TBuilder : IAbstractBuilder<TEntity>
{
    TBuilder New();
    TBuilder Edit(TEntity instance);
}