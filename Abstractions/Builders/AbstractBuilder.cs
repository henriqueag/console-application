namespace ConsoleApplication.Abstractions.Builders;

public class AbstractBuilder<T> : IAbstractBuilder<T>
    where T : class, new()
{
    protected T Instance;

    protected AbstractBuilder() : this(null) { }

    private AbstractBuilder(T instance) => Instance = instance ?? new();

    public T Build()
    {
        return Instance;
    }

    public void Edit(T instance)
    {
        Instance = instance;
    }
}