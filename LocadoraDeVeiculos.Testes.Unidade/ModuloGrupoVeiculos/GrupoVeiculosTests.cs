using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;

namespace LocadoraDeVeiculos.Testes.Unidade.ModuloGrupoVeiculos;

[TestClass]
[TestCategory("Unidade")]
public class GrupoVeiculosTests
{

    [TestMethod]
    [DataRow("SUV")]
    public void Deve_Criar_Instancia_Valida(string nomeGrupo)
    {
        var grupo = new GrupoVeiculos(nomeGrupo);

        var erros = grupo.Validar();

        Assert.AreEqual(0, erros.Count);
    }

    [TestMethod]
    [DataRow("", "O nome é obrigatório")]
    [DataRow("AB", "O nome deve ser maior que três letras")]
    public void Deve_Criar_Instancia_Com_Erro(string nomeGrupo, string mensagemErro)
    {
        var grupo = new GrupoVeiculos(nomeGrupo);

        var erros = grupo.Validar();

        Assert.AreEqual(1, erros.Count);
        Assert.AreEqual(mensagemErro, erros[0]);
    }
}


