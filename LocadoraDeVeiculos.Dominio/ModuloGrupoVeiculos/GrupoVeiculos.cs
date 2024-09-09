using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloVeiculo;

namespace LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;

public class GrupoVeiculos : EntidadeBase
{
    public string Nome { get; set; }

    public List<Veiculo> Veiculos { get; set; } = [];

    protected GrupoVeiculos() { }

    public GrupoVeiculos(string nome)
    {
        Nome = nome;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrEmpty(Nome))
            erros.Add("O nome é obrigatório");

        else if (Nome.Length < 3)
            erros.Add("O nome deve ser maior que três letras");

        return erros;
    }
}
