using LocadoraDeVeiculos.Testes.e2e.Compartilhado;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.GrupoVeiculo;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.PlanoCobranca;

namespace LocadoraDeVeiculos.Testes.e2e.PlanoCobranca;

[TestClass]
public class AoExcluirPlanoCobranca : TestFixture
{
    FormularioGrupoVeiculoPageObject grupoVeiculoPage;
    FormularioPlanoCobrancaPageObject planoCobrancaPage;

    private ExcluirPlanoCobrancaPageObject excluirPlanoCobrancaPage;

    public AoExcluirPlanoCobranca()
    {
        grupoVeiculoPage = new FormularioGrupoVeiculoPageObject(driver);
        planoCobrancaPage = new FormularioPlanoCobrancaPageObject(driver);

        excluirPlanoCobrancaPage = new ExcluirPlanoCobrancaPageObject(driver);
    }

    [TestMethod]
    public void Dado_info_validas_nao_deve_mostrar_PlanoDeCobranca_na_listagem()
    {
        var nomeGrupo = "Esportivo";

        grupoVeiculoPage.Visitar();
        grupoVeiculoPage.PreencherFormulario(nomeGrupo);
        grupoVeiculoPage.SubmeterFormulario();

        planoCobrancaPage.Visitar();
        string precoDiarioPlanoDiario = "123456";
        planoCobrancaPage.PreencherFormulario(nomeGrupo, precoDiarioPlanoDiario, "10", "10", "10", "10", "10");
        planoCobrancaPage.SubmeterFormulario();

        int id = planoCobrancaPage.GetId(precoDiarioPlanoDiario);

        excluirPlanoCobrancaPage.Visitar(id);
        excluirPlanoCobrancaPage.ConfirmarExclusao();

        Assert.IsFalse(driver.PageSource.Contains(precoDiarioPlanoDiario));
    }
}
