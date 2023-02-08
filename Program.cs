using ConsoleApplication.Abstractions.Factories;
using ConsoleApplication.Builders;
using ConsoleApplication.Entities;
using Microsoft.Extensions.DependencyInjection;
using static ConsoleApplication.Entities.Cliente;

namespace console_application;

class Program
{
    private static readonly IServiceProvider _services = GetServiceProvider();
    private static readonly IDictionary<int, Cliente> _database = new Dictionary<int, Cliente>()
    {
        { Database[1].Id, Database[1] },
        { Database[2].Id, Database[2] },
        { Database[3].Id, Database[3] },
        { Database[4].Id, Database[4] },
    };

    static void Main(string[] args)
    {
        do
        {
            Console.Clear();

            Console.WriteLine("CADASTRO DE CLIENTE");
            Console.WriteLine("".PadRight(30, '='));

            Console.WriteLine("SELECIONE UMA OPÇÃO:");
            Console.WriteLine($"1 - NOVO  |  2 - EDITAR  |  3 - LISTAR  |  4 - SAIR");
            var opcao = int.Parse(Console.ReadLine());

            ExecutarOpcao(opcao);

            Console.ReadLine();
        } while (true);
    }

    static void ExecutarOpcao(int opcao)
    {
        switch (opcao)
        {
            case 1:
                NovoCadastro();
                break;
            case 2:
                EditarCadastro();
                break;
            case 3:
                ListarCadastros();
                break;
            case 4:
                Environment.Exit(0);
                break;
        }
    }

    static void NovoCadastro()
    {
        Console.Write("NOME: ");
        var nome = Console.ReadLine();
        Console.Write("IDADE: ");
        var idade = int.Parse(Console.ReadLine());

        var factory = _services.GetRequiredService<IEntityFactory<Cliente, IClienteBuilder>>();
        var instance = factory.New()
            .WithId(_database.Keys.Last() + 1)
            .WithNome(nome.ToUpper())
            .WithIdade(idade)
            .Build();

        _database.Add(instance.Id, instance);

        Console.WriteLine("CADASTRO REALIZADO COM SUCESSO");
        Console.WriteLine(instance);
    }

    static void EditarCadastro()
    {
        Console.Write("INFORME O ID DO CLIENTE: ");
        var clienteId = int.Parse(Console.ReadLine());

        var toEdit = _database[clienteId];
        Console.WriteLine($"CADASTRO ENCONTRADO: {toEdit}");
        Console.WriteLine("".PadRight(30, '-'));

        Console.Write("NOME: ");
        var nome = Console.ReadLine();
        Console.Write("IDADE: ");
        var idade = int.Parse(Console.ReadLine());

        var factory = _services.GetRequiredService<IEntityFactory<Cliente, IClienteBuilder>>();
        var instance = factory.Edit(toEdit)
            .WithNome(nome.ToUpper())
            .WithIdade(idade)
            .Build();

        Console.WriteLine("CADASTRO EDITADO COM SUCESSO");
        Console.WriteLine(instance);
    }

    static void ListarCadastros()
    {
        Console.WriteLine("LISTAGEM DE CLIENTES");
        Console.WriteLine();

        var padding = 61;
        Console.WriteLine("".PadRight(padding, '-'));

        Console.Write($"{" Id",-6}");
        Console.Write($"|{" Nome",-27}");
        Console.Write($"|{" Idade",-9}");
        Console.Write($"|{" Data Cadastro",-15:d}|");
        Console.WriteLine();
        foreach (var cliente in _database.Values)
        {
            Console.WriteLine("".PadRight(padding, '-'));

            Console.Write($" {cliente.Id,-5}|");
            Console.Write($" {cliente.Nome,-26}|");
            Console.Write($" {cliente.Idade,-8}|");
            Console.Write($" {cliente.CriadoEm,-14:d}|");

            Console.WriteLine();
        }

        Console.WriteLine("".PadRight(padding, '-'));
    }

    static IServiceProvider GetServiceProvider()
    {
        return new ServiceCollection()
            .AddTransient(typeof(IEntityFactory<,>), typeof(EntityFactory<,>))
            .AddTransient<IClienteBuilder, ClienteBuilder>()
            .BuildServiceProvider();
    }
}