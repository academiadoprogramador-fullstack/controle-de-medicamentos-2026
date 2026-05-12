using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

public class Medicamento : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public Fornecedor Fornecedor { get; set; } = null!;
    public List<RequisicaoBase> Requisicoes { get; set; } = new List<RequisicaoBase>();
    public uint QuantidadeEmEstoque
    {
        get
        {
            uint quantidadeEmEstoque = 0;

            foreach (RequisicaoBase req in Requisicoes)
            {
                if (req is RequisicaoEntrada reqEntrada)
                    quantidadeEmEstoque += reqEntrada.Quantidade;

                else if (req is RequisicaoSaida reqSaida)
                {
                    foreach (MedicamentoPrescrito medPresc in reqSaida.MedicamentosPrescritos)
                    {
                        if (medPresc.Medicamento == this)
                            quantidadeEmEstoque -= medPresc.Quantidade;
                    }
                }
            }

            return quantidadeEmEstoque;
        }
    }

    public Medicamento()
    {
    }

    public Medicamento(string nome, string descricao, Fornecedor fornecedor) : this()
    {
        Nome = nome;
        Descricao = descricao;
        Fornecedor = fornecedor;
    }

    public void RegistrarRequisicao(RequisicaoBase requisicao)
    {
        Requisicoes.Add(requisicao);
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Descricao) || Descricao.Length < 5 || Descricao.Length > 255)
            erros.Add("O campo \"Descricão\" deve conter entre 5 e 255 caracteres.");

        if (Fornecedor == null)
            erros.Add("O campo \"Fornecedor\" deve ser preenchido.");

        return erros;
    }

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        Medicamento medicamentoAtualizado = (Medicamento)entidadeAtualizada;

        Nome = medicamentoAtualizado.Nome;
        Descricao = medicamentoAtualizado.Descricao;
        Fornecedor = medicamentoAtualizado.Fornecedor;
    }
}
