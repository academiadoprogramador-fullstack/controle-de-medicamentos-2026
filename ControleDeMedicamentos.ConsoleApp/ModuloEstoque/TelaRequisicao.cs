using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class TelaRequisicao : ITelaOpcoes
{
    private readonly IRepositorioRequisicao repositorioRequisicao;
    private readonly IRepositorio<Funcionario> repositorioFuncionario;
    private readonly IRepositorio<Medicamento> repositorioMedicamento;

    public TelaRequisicao(
        IRepositorioRequisicao repositorioRequisicao,
        IRepositorio<Funcionario> repositorioFuncionario,
        IRepositorio<Medicamento> repositorioMedicamento
    )
    {
        this.repositorioRequisicao = repositorioRequisicao;
        this.repositorioFuncionario = repositorioFuncionario;
        this.repositorioMedicamento = repositorioMedicamento;
    }

    public string? ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Estoque");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Cadastrar requisição de entrada");
        Console.WriteLine($"2 - Visualizar requisições de entrada");
        Console.WriteLine($"3 - Cadastrar requisição de saída");
        Console.WriteLine($"4 - Visualizar requisições de saída");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    public void CadastrarRequisicaoEntrada()
    {
        ExibirCabecalho("Cadastro de Requisição de Entrada");

        // 1. Seleciona um funcionário válido
        VisualizarFuncionarios();

        string idFuncionario;

        do
        {
            Console.Write("Digite o ID do funcionário que fará a requisição: ");
            idFuncionario = Console.ReadLine() ?? string.Empty;

            if (idFuncionario.Length == 7)
                break;
        } while (true);

        Funcionario funcionario = repositorioFuncionario.SelecionarPorId(idFuncionario)!;

        // 2. Seleciona um medicamento válido
        VisualizarMedicamentos();

        string idMedicamento;

        do
        {
            Console.Write("Digite o ID do medicamento que será requisitado: ");
            idMedicamento = Console.ReadLine() ?? string.Empty;

            if (idMedicamento.Length == 7)
                break;
        } while (true);

        Medicamento medicamento = repositorioMedicamento.SelecionarPorId(idMedicamento)!;

        // 3. Obtém a quantidade do medicamento desejada
        Console.Write("Digite a quantidade do medicamento que será requisitada: ");
        uint quantidade = Convert.ToUInt32(Console.ReadLine());

        RequisicaoEntrada requisicaoEntrada = new RequisicaoEntrada(
            funcionario,
            medicamento,
            quantidade
        );

        repositorioRequisicao.Cadastrar(requisicaoEntrada);

        Notificador.ExibirMensagem($"O registro \"{requisicaoEntrada.Id}\" foi cadastrado com sucesso!");
    }

    public void VisualizarRequisicoesEntrada()
    {
        ExibirCabecalho("Visualização de Requisições de Entrada");

        Console.WriteLine(
            "{0, -7} | {1, -18} | {2, -20} | {3, -20} | {4, -18}",
            "Id", "Data de Criação", "Funcionário", "Medicamento", "Quantidade"
        );

        List<RequisicaoEntrada> requisicoes = repositorioRequisicao.SelecionarRequisicoesEntrada();

        foreach (RequisicaoEntrada req in requisicoes)
        {
            Console.WriteLine(
                "{0, -7} | {1, -18} | {2, -20} | {3, -20} | {4, -18}",
                req.Id,
                req.DataCriacao.ToShortDateString(),
                req.Funcionario.Nome,
                req.Medicamento.Nome,
                req.Quantidade
            );
        }

        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    private void VisualizarFuncionarios()
    {
        Console.WriteLine("---------------------------------");

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -17}",
            "Id", "Nome", "Telefone", "CPF"
        );

        List<Funcionario> registros = repositorioFuncionario.SelecionarTodos();

        foreach (Funcionario f in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -17}",
                f.Id, f.Nome, f.Telefone, f.Cpf
            );
        }

        Console.WriteLine("---------------------------------");
    }

    private void VisualizarMedicamentos()
    {
        Console.WriteLine("---------------------------------");

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -20} | {3, -20}",
            "Id", "Nome", "Fornecedor", "Descrição"
        );

        List<Medicamento> registros = repositorioMedicamento.SelecionarTodos();

        foreach (Medicamento m in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -20} | {3, -20}",
                m.Id, m.Nome, m.Fornecedor.Nome, m.Descricao
            );
        }

        Console.WriteLine("---------------------------------");
    }

    protected void ExibirCabecalho(string titulo)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Estoque");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(titulo);
        Console.WriteLine("---------------------------------");
    }
}
