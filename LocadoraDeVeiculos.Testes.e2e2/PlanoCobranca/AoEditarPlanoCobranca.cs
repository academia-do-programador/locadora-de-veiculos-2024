using LocadoraDeVeiculos.Testes.e2e.Compartilhado;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.GrupoVeiculo;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.PlanoCobranca;

namespace LocadoraDeVeiculos.Testes.e2e.PlanoCobranca;

[TestClass]
public class AoEditarPlanoCobranca : TestFixture
{
    private FormularioPlanoCobrancaPageObject planoCobrancaPage;
    private FormularioGrupoVeiculoPageObject grupoVeiculoPage;

    public AoEditarPlanoCobranca()
    {
        planoCobrancaPage = new FormularioPlanoCobrancaPageObject(driver);
        grupoVeiculoPage = new FormularioGrupoVeiculoPageObject(driver);
    }

    [TestMethod]
    public void Dado_info_validas_deve_mostrar_PlanoCobranca_atualizado_na_listagem()
    {
        //arrange
        string nomeGrupoVeiculo = "Esportivo";

        grupoVeiculoPage.Visitar();
        grupoVeiculoPage.PreencherFormulario(nomeGrupoVeiculo);
        grupoVeiculoPage.SubmeterFormulario();

        string precoDiarioPlanoDiario = "10";
        planoCobrancaPage.Visitar();
        planoCobrancaPage.PreencherFormulario(nomeGrupoVeiculo, precoDiarioPlanoDiario, "10", "10", "10", "10", "10");
        planoCobrancaPage.SubmeterFormulario();

        int id = planoCobrancaPage.GetId(precoDiarioPlanoDiario);

        planoCobrancaPage.Visitar(id);
        planoCobrancaPage.PreencherFormulario(nomeGrupoVeiculo, "20", "10", "10", "10", "10", "10");

        //action
        planoCobrancaPage.SubmeterFormulario();

        //assert
        Assert.IsTrue(driver.PageSource.Contains("Preço Diário (Diário): R$ 20,00"));
    }
}