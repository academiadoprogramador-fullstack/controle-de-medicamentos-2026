using System.Text.RegularExpressions;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

public class Funcionario : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;

    public Funcionario() { }

    public Funcionario(string nome, string telefone, string cpf) : this()
    {
        Nome = nome;
        Telefone = telefone;
        Cpf = cpf;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");

        if (!Regex.IsMatch(Telefone, @"^\(?\d{2}\)? \d{4,5}-\d{4}$"))
            erros.Add("O campo \"Telefone\" deve ser estar no formato (DDD) 90000-0000.");

        if (!Regex.IsMatch(Cpf, @"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$"))
            erros.Add("O campo \"CPF\" deve conter 11 dígitos.");

        return erros;
    }

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        Funcionario funcionarioAtualizado = (Funcionario)entidadeAtualizada;

        Nome = funcionarioAtualizado.Nome;
        Telefone = funcionarioAtualizado.Telefone;
        Cpf = funcionarioAtualizado.Cpf;
    }
}
