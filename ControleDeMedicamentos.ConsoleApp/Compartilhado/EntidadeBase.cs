using System.Security.Cryptography;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public abstract class EntidadeBase
{
    public string Id { get; set; } = GeradorIds.GerarIdCurto();

    public abstract List<string> Validar();
    public abstract void AtualizarDados(EntidadeBase entidadeAtualizada);
}
