using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.Utilidades;

public class TelaPrincipal
{
    private readonly IRepositorio<Paciente> repositorioPaciente;
    private readonly IRepositorio<Funcionario> repositorioFuncionario;
    private readonly IRepositorio<Fornecedor> repositorioFornecedor;
    private readonly IRepositorio<Medicamento> repositorioMedicamento;
    private readonly IRepositorioRequisicao repositorioRequisicao;

    public TelaPrincipal(
        IRepositorio<Paciente> repositorioPaciente,
        IRepositorio<Funcionario> repositorioFuncionario,
        IRepositorio<Fornecedor> repositorioFornecedor,
        IRepositorio<Medicamento> repositorioMedicamento,
        IRepositorioRequisicao repositorioRequisicao
    )
    {
        this.repositorioPaciente = repositorioPaciente;
        this.repositorioFuncionario = repositorioFuncionario;
        this.repositorioFornecedor = repositorioFornecedor;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioRequisicao = repositorioRequisicao;
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

        if (opcaoMenuPrincipal == "2")
            return new TelaFuncionario(repositorioFuncionario);

        if (opcaoMenuPrincipal == "3")
            return new TelaFornecedor(repositorioFornecedor);

        if (opcaoMenuPrincipal == "4")
            return new TelaMedicamento(repositorioMedicamento, repositorioFornecedor);

        if (opcaoMenuPrincipal == "5")
        {
            return new TelaRequisicao(
                repositorioRequisicao,
                repositorioFuncionario,
                repositorioMedicamento,
                repositorioPaciente
            );
        }

        return null;
    }
}
