namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public interface IRepositorioRequisicao
{
    void Cadastrar(RequisicaoBase requisicao);
    List<RequisicaoEntrada> SelecionarRequisicoesEntrada();
    List<RequisicaoSaida> SelecionarRequisicoesSaida();
}
