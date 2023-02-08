using ConsoleApplication.Abstractions.Builders;
using ConsoleApplication.Entities;

namespace ConsoleApplication.Builders;

public interface IClienteBuilder : IAbstractBuilder<Cliente>
{
    IClienteBuilder WithId(int id);
    IClienteBuilder WithNome(string nome);
    IClienteBuilder WithIdade(int idade);
}