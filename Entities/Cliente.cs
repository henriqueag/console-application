using ConsoleApplication.Abstractions.Builders;
using ConsoleApplication.Builders;

namespace ConsoleApplication.Entities;

public class Cliente
{
    public static Dictionary<int, Cliente> Database =>
        new()
        {
            { 1, new(1, "Henrique Aguiar", 24) },
            { 2, new(2, "Miriam Vitoria", 25) },
            { 3, new(3, "Estefane Batista", 24) },
            { 4, new(4, "Juliano Guimaraes", 31) },
        };

    public Cliente()
    {
        CriadoEm = DateTime.Now;
    }

    protected Cliente(int id, string nome, int idade)
    {
        Id = id;
        Nome = nome;
        Idade = idade;
        CriadoEm = DateTime.Now;
    }

    public int Id { get; private set; }
    public string Nome { get; private set; }
    public int Idade { get; private set; }
    public DateTime CriadoEm { get; private set; }

    public override string ToString()
    {
        return $"{Id}, {Nome}, {Idade}, {CriadoEm:d}";
    }

    public class ClienteBuilder : AbstractBuilder<Cliente>, IClienteBuilder
    {
        public IClienteBuilder WithId(int id)
        {
            Instance.Id = id;
            return this;
        }

        public IClienteBuilder WithIdade(int idade)
        {
            Instance.Idade = idade;
            return this;
        }

        public IClienteBuilder WithNome(string nome)
        {
            Instance.Nome = nome;
            return this;
        }
    }
}