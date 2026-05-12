using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.Utilidades;

public class TelaPrincipal
{
    private readonly IRepositorio<Paciente> repositorioPaciente;
    private readonly IRepositorio<Fornecedor> repositorioFornecedor;

    public TelaPrincipal(IRepositorio<Paciente> repositorioPaciente, IRepositorio<Fornecedor> repositorioFornecedor)
    {
        this.repositorioPaciente = repositorioPaciente;
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public ITelaOpcoes? ApresentarMenuOpcoesPrincipal()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Controle de Medicamentos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Gestão de Pacientes");
        Console.WriteLine("2 - Gestão de Funcionários");
        Console.WriteLine("3 - Gestão de Fornecedores");
        Console.WriteLine("4 - Gestão de Medicamentos");
        Console.WriteLine("5 - Gestão de Estoque");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

        if (opcaoMenuPrincipal == "1")
            return new TelaPaciente(repositorioPaciente);

        if (opcaoMenuPrincipal == "3")
            return new TelaFornecedor(repositorioFornecedor);

        return null;
    }
}
