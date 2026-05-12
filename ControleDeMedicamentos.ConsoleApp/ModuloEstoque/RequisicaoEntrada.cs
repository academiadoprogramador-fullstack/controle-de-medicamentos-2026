using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class RequisicaoEntrada : RequisicaoBase
{
    public Funcionario Funcionario { get; set; } = null!;
    public Medicamento Medicamento { get; set; } = null!;
    public uint Quantidade { get; set; } = 0;

    public RequisicaoEntrada()
    {
        Tipo = TipoRequisicao.Entrada;
    }

    public RequisicaoEntrada(
        Funcionario funcionario,
        Medicamento medicamento,
        uint quantidade
    ) : this()
    {
        Funcionario = funcionario;
        Medicamento = medicamento;
        Quantidade = quantidade;

        Medicamento.RegistrarRequisicao(this);
    }
}
