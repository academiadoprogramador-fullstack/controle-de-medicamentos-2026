
using System.Text.Json;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

ContextoJson contexto = new ContextoJson();

try
{
    contexto.Carregar();
}
catch (JsonException)
{
    Notificador.ExibirMensagem("O arquivo de armazenamento está corrompido! Contate o suporte.");
    return;
}

IRepositorio<Paciente> repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);
IRepositorio<Funcionario> repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contexto);
IRepositorio<Fornecedor> repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
IRepositorio<Medicamento> repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
IRepositorioRequisicao repositorioRequisicao = new RepositorioRequisicaoEmArquivo(contexto);

TelaPrincipal telaPrincipal = new TelaPrincipal(
    repositorioPaciente,
    repositorioFuncionario,
    repositorioFornecedor,
    repositorioMedicamento,
    repositorioRequisicao
);

while (true)
{
    ITelaOpcoes? telaSelecionada = telaPrincipal.ApresentarMenuOpcoesPrincipal();

    if (telaSelecionada == null)
    {
        Console.Clear();
        break;
    }

    while (true)
    {
        string? opcaoSubMenu = telaSelecionada.ObterOpcaoMenu();

        if (opcaoSubMenu == "S")
        {
            Console.Clear();
            break;
        }

        if (telaSelecionada is ITelaCrud telaCrud)
        {
            if (opcaoSubMenu == "1")
                telaCrud.Cadastrar();

            else if (opcaoSubMenu == "2")
                telaCrud.Editar();

            else if (opcaoSubMenu == "3")
                telaCrud.Excluir();

            else if (opcaoSubMenu == "4")
                telaCrud.VisualizarTodos(deveExibirCabecalho: true);
        }

        else if (telaSelecionada is TelaRequisicao telaRequisicao)
        {
            if (opcaoSubMenu == "1")
                telaRequisicao.CadastrarRequisicaoEntrada();
            else if (opcaoSubMenu == "2")
                telaRequisicao.VisualizarRequisicoesEntrada();
        }
    }
}
