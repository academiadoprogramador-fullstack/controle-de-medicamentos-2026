using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaOpcoes, ITelaCrud
{
    private readonly IRepositorio<Fornecedor> repositorioFornecedor;

    public TelaMedicamento(
        IRepositorio<Medicamento> repositorioMedicamento,
        IRepositorio<Fornecedor> repositorioFornecedor
    ) : base("Medicamento", repositorioMedicamento)
    {
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Medicamentos");

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -20} | {3, -20}",
            "Id", "Nome", "Fornecedor", "Descrição"
        );

        List<Medicamento> registros = repositorio.SelecionarTodos();

        foreach (Medicamento m in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -20} | {3, -20}",
                m.Id, m.Nome, m.Fornecedor.Nome, m.Descricao
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Medicamento ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do medicamento: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite a descrição do medicamento: ");
        string descricao = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("---------------------------------");

        VisualizarFornecedores();

        Console.WriteLine("---------------------------------");


        string idSelecionado;

        do
        {
            Console.Write("Digite o ID do fornecedor que deseja selecionar: ");
            idSelecionado = Console.ReadLine() ?? string.Empty;

            if (idSelecionado.Length == 7)
                break;
        } while (true);

        Fornecedor fornecedor = repositorioFornecedor.SelecionarPorId(idSelecionado)!;

        return new Medicamento(nome, descricao, fornecedor);
    }

    private void VisualizarFornecedores()
    {
        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -17}",
            "Id", "Nome", "Telefone", "CNPJ"
        );

        List<Fornecedor> registros = repositorioFornecedor.SelecionarTodos();

        foreach (Fornecedor f in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -17}",
                f.Id, f.Nome, f.Telefone, f.Cnpj
            );
        }
    }
}
