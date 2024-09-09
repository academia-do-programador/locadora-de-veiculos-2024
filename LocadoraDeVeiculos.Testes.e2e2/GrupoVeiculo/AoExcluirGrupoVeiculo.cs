using LocadoraDeVeiculos.Testes.e2e.Compartilhado;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.GrupoVeiculo;

namespace LocadoraDeVeiculos.Testes.e2e.GrupoVeiculo;

[TestClass]
public class AoExcluirGrupoVeiculo : TestFixture
{
    FormularioGrupoVeiculoPageObject grupoVeiculoPage;
    ExcluirGrupoVeiculoPageObject excluirGrupoVeiculoPage;

    public AoExcluirGrupoVeiculo()
    {
        grupoVeiculoPage = new FormularioGrupoVeiculoPageObject(driver);
        excluirGrupoVeiculoPage = new ExcluirGrupoVeiculoPageObject(driver);
    }

    [TestMethod]
    [DataRow("Carros de Luxo")]
    public void Dado_info_validas_deve_excluir_grupo_na_listagem(string nomeGrupo)
    {
        grupoVeiculoPage.Visitar();
        grupoVeiculoPage.PreencherFormulario(nomeGrupo);
        grupoVeiculoPage.SubmeterFormulario();

        var id = grupoVeiculoPage.GetId(nomeGrupo);

        excluirGrupoVeiculoPage.Visitar(id);
        excluirGrupoVeiculoPage.ConfirmarExclusao();

        Assert.IsFalse(driver.PageSource.Contains(nomeGrupo));
    }

}