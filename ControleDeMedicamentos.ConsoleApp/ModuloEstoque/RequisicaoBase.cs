using System.Text.Json.Serialization;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

// atributo
[JsonPolymorphic(TypeDiscriminatorPropertyName = "Tipo")]
[JsonDerivedType(typeof(RequisicaoEntrada), (int)TipoRequisicao.Entrada)]
public abstract class RequisicaoBase
{
    public string Id { get; set; } = GeradorIds.GerarIdCurto();
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public TipoRequisicao Tipo { get; set; } = TipoRequisicao.Indefinido;
}
