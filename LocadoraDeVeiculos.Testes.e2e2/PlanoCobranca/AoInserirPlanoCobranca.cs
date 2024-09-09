using LocadoraDeVeiculos.Testes.e2e.Compartilhado;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.GrupoVeiculo;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.PlanoCobranca;
using OpenQA.Selenium;

namespace LocadoraDeVeiculos.Testes.e2e.PlanoCobranca;

[TestClass]
public class AoInserirPlanoCobranca : TestFixture
{
    FormularioGrupoVeiculoPageObject grupoVeiculoPage;
    FormularioPlanoCobrancaPageObject planoCobrancaPage;

    public AoInserirPlanoCobranca()
    {
        grupoVeiculoPage = new FormularioGrupoVeiculoPageObject(driver);
        planoCobrancaPage = new FormularioPlanoCobrancaPageObject(driver);
    }

    [TestMethod]
    [DataRow("Carros de Luxo")]
    public void Dado_info_validas_deve_mostrar_PlanoCobranca_na_listagem(string nomeGrupo)
    {
        grupoVeiculoPage.Visitar();
        grupoVeiculoPage.PreencherFormulario(nomeGrupo);
        grupoVeiculoPage.SubmeterFormulario();

        planoCobrancaPage.Visitar();
        planoCobrancaPage.PreencherFormulario(nomeGrupo, "10", "10", "10", "10", "10", "10");

        planoCobrancaPage.SubmeterFormulario();

        Assert.IsTrue(driver.PageSource.Contains($"Grupo de Veículos: {nomeGrupo}"));
    }

    [TestMethod]
    public void Dado_info_invalidas_deve_permanecer_no_formulario_do_PlanoDeCobranca()
    {
        planoCobrancaPage.Visitar();

        planoCobrancaPage.PreencherFormulario("0", "10", "10", "10", "10", "10", "10");

        planoCobrancaPage.SubmeterFormulario();

        var elemento = driver.FindElement(By.CssSelector(".field-validation-error[data-valmsg-for='GrupoVeiculosId']"));

        Assert.AreEqual("O grupo de veículos é obrigatório", elemento.Text);
        Assert.IsTrue(elemento.Displayed);
    }

}