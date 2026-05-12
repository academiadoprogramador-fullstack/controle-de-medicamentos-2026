using System.Text.Json.Serialization;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$tipo")]
[JsonDerivedType(typeof(RequisicaoEntrada), "entrada")]
[JsonDerivedType(typeof(RequisicaoSaida), "saida")]
public abstract class RequisicaoBase
{
    public string Id { get; set; } = GeradorIds.GerarIdCurto();
    public DateTime DataCriacao { get; set; } = DateTime.Now;
}
