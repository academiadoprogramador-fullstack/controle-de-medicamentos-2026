using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

public class TelaFuncionario : TelaBase<Funcionario>, ITelaOpcoes, ITelaCrud
{
    public TelaFuncionario(IRepositorio<Funcionario> repositorio) : base("Funcionário", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Funcionários");

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -17}",
            "Id", "Nome", "Telefone", "CPF"
        );

        List<Funcionario> registros = repositorio.SelecionarTodos();

        foreach (Funcionario f in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -17}",
                f.Id, f.Nome, f.Telefone, f.Cpf
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Funcionario ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do funcionário: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o telefone do funcionário: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o CPF do funcionário: ");
        string cpf = Console.ReadLine() ?? string.Empty;

        return new Funcionario(nome, telefone, cpf);
    }

    protected override List<string> ValidarRegistroDuplicado(Funcionario novaEntidade, string? idIgnorado = null)
    {
        List<string> erros = [];

        List<Funcionario> registros = repositorio.SelecionarTodos();

        foreach (Funcionario f in registros)
        {
            if (f.Id != idIgnorado && f.Cpf == novaEntidade.Cpf)
                erros.Add("Já existe o registro de um funcionário com o CPF informado.");
        }

        return erros;
    }
}
