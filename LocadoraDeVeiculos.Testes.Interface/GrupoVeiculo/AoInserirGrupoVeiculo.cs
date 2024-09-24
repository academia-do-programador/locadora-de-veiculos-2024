using LocadoraDeVeiculos.Testes.Interface.Compartilhado;
using LocadoraDeVeiculos.Testes.Interface.Compartilhado.PageObjects;

namespace LocadoraDeVeiculos.Testes.Interface.GrupoVeiculo;

[TestClass]
public class AoInserirGrupoVeiculo : TestFixture
{
    private readonly ForumularioGrupoVeiculoPageObject grupoVeiculoPage;

    public AoInserirGrupoVeiculo()
    {
        grupoVeiculoPage = new ForumularioGrupoVeiculoPageObject(driver);
    }

    [TestMethod]
    [DataRow("Esportivo")]
    [DataRow("Utilitario")]
    public void Dado_info_validas_deve_apresentar_grupo_veiculo_na_listagem(string nomeGrupo)
    {
        //arrange
        grupoVeiculoPage.Visitar();
        grupoVeiculoPage.PreencherFormulario(nomeGrupo);

        //action
        grupoVeiculoPage.SubmeterFormulario();

        //assert
        Assert.IsTrue(driver.PageSource.Contains(nomeGrupo));
    }
}