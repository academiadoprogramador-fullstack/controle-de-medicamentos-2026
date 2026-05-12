using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class RepositorioRequisicaoEmArquivo : IRepositorioRequisicao
{
    protected readonly ContextoJson contexto;
    protected readonly List<RequisicaoBase> registros;

    public RepositorioRequisicaoEmArquivo(ContextoJson contexto)
    {
        this.contexto = contexto;
        registros = contexto.Requisicoes;
    }

    public void Cadastrar(RequisicaoBase requisicao)
    {
        registros.Add(requisicao);

        contexto.Salvar();
    }

    public List<RequisicaoEntrada> SelecionarRequisicoesEntrada()
    {
        List<RequisicaoEntrada> requisicoesEntrada = new List<RequisicaoEntrada>();

        foreach (RequisicaoBase req in registros)
        {
            if (req is RequisicaoEntrada reqEntrada)
                requisicoesEntrada.Add(reqEntrada);
        }

        return requisicoesEntrada;
    }

    public List<RequisicaoSaida> SelecionarRequisicoesSaida()
    {
        List<RequisicaoSaida> requisicoesSaida = new List<RequisicaoSaida>();

        foreach (RequisicaoBase req in registros)
        {
            if (req is RequisicaoSaida reqSaida)
                requisicoesSaida.Add(reqSaida);
        }

        return requisicoesSaida;
    }
}
