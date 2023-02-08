namespace ConsoleApplication.Abstractions.Builders;

public interface IAbstractBuilder<T> where T : class, new()
{
    void Edit(T instance);
    T Build();
}