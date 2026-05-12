using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class TelaRequisicao : ITelaOpcoes
{
    private readonly IRepositorioRequisicao repositorioRequisicao;
    private readonly IRepositorio<Funcionario> repositorioFuncionario;
    private readonly IRepositorio<Medicamento> repositorioMedicamento;
    private readonly IRepositorio<Paciente> repositorioPaciente;

    public TelaRequisicao(
        IRepositorioRequisicao repositorioRequisicao,
        IRepositorio<Funcionario> repositorioFuncionario,
        IRepositorio<Medicamento> repositorioMedicamento,
        IRepositorio<Paciente> repositorioPaciente
    )
    {
        this.repositorioRequisicao = repositorioRequisicao;
        this.repositorioFuncionario = repositorioFuncionario;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioPaciente = repositorioPaciente;
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

    public void CadastrarRequisicaoSaida()
    {
        ExibirCabecalho("Cadastro de Requisição de Saída");

        VisualizarPacientes();

        string idPaciente;

        do
        {
            Console.Write("Digite o ID do paciente que receberá a medicação: ");
            idPaciente = Console.ReadLine() ?? string.Empty;

            if (idPaciente.Length == 7)
                break;
        } while (true);

        Paciente paciente = repositorioPaciente.SelecionarPorId(idPaciente)!;

        List<MedicamentoPrescrito> medicamentosPrescritos = new List<MedicamentoPrescrito>();

        do
        {
            Console.WriteLine($"Paciente: {paciente.Nome}");
            Console.WriteLine("---------------------------------");

            if (medicamentosPrescritos.Count > 0)
            {
                Console.WriteLine("Medicamentos selecionados");
                Console.WriteLine("---------------------------------");

                Console.WriteLine(
                    "{0, -30} | {1, -30}",
                    "Medicamento", "Quantidade"
                );

                foreach (MedicamentoPrescrito medPresc in medicamentosPrescritos)
                {
                    Console.WriteLine(
                        "{0, -30} | {1, -30}",
                        medPresc.Medicamento.Nome, medPresc.Quantidade
                    );
                }

                Console.WriteLine("---------------------------------");
            }

            Console.WriteLine("Medicamentos para adicionar");

            VisualizarMedicamentos();

            Console.Write("Digite o ID do medicamento que deseja retirar (ou S para sair): ");
            string idMedicamento = Console.ReadLine() ?? string.Empty;

            if (idMedicamento.ToUpper() == "S")
                break;

            Medicamento medicamentoSelecionado = repositorioMedicamento.SelecionarPorId(idMedicamento)!;

            Console.Write("Digite a quantidade do medicamento que deseja retirar: ");
            uint quantidade = Convert.ToUInt32(Console.ReadLine());

            MedicamentoPrescrito medPrescrito = new MedicamentoPrescrito(medicamentoSelecionado, quantidade);

            medicamentosPrescritos.Add(medPrescrito);
        } while (true);

        RequisicaoSaida requisicaoSaida = new RequisicaoSaida(paciente, medicamentosPrescritos);

        repositorioRequisicao.Cadastrar(requisicaoSaida);

        Notificador.ExibirMensagem($"O registro \"{requisicaoSaida.Id}\" foi cadastrado com sucesso!");
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

    public void VisualizarRequisicoesSaida()
    {
        ExibirCabecalho("Visualização de Requisições de Saída");

        List<RequisicaoSaida> requisicoes = repositorioRequisicao.SelecionarRequisicoesSaida();

        Console.WriteLine(
            "{0, -7} | {1, -18} | {2, -20} | {3, -40}",
            "Id", "Data de Criação", "Paciente", "Medicamentos Requisitados"
        );

        foreach (RequisicaoSaida req in requisicoes)
        {
            List<string> medicamentosStr = new List<string>();

            foreach (MedicamentoPrescrito medPresc in req.MedicamentosPrescritos)
            {
                string medicamentoStr = $"({medPresc.Medicamento.Nome}, {medPresc.Quantidade})";

                medicamentosStr.Add(medicamentoStr);
            }

            Console.Write("{0, -7} | ", req.Id);
            Console.Write("{0, -18} | ", req.DataCriacao.ToShortDateString());
            Console.Write("{0, -20} | ", req.Paciente.Nome);
            Console.Write("{0, -40}", string.Join(", ", medicamentosStr));

            Console.WriteLine();
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

    private void VisualizarPacientes()
    {
        Console.WriteLine("---------------------------------");

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -17} | {4, -17}",
            "Id", "Nome", "Telefone", "CPF", "Cartão SUS"
        );

        List<Paciente> registros = repositorioPaciente.SelecionarTodos();

        foreach (Paciente p in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -17} | {4, -17}",
                p.Id, p.Nome, p.Telefone, p.Cpf, p.CartaoSus
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
