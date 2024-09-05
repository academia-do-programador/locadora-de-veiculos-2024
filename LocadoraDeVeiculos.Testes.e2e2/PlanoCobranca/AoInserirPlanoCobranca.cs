using LocadoraDeVeiculos.Testes.e2e.Compartilhado;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.GrupoVeiculo;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.PlanoCobranca;

namespace LocadoraDeVeiculos.Testes.e2e.PlanoCobranca;

[TestClass]
public class AoInserirPlanoCobranca : TextFixture
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
        string precoDiarioPlanoDiario = "100";
        planoCobrancaPage.PreencherFormulario(nomeGrupo, precoDiarioPlanoDiario, "10", "10", "10", "10", "10");

        planoCobrancaPage.SubmeterFormulario();

        Assert.IsTrue(driver.PageSource.Contains($"Grupo de Veículos: {nomeGrupo}"));
    }

    [TestMethod]
    public void Dado_info_invalidas_deve_permanecer_na_pagina()
    {
        planoCobrancaPage.Visitar();

        planoCobrancaPage.PreencherFormulario("0", "10", "10", "10", "10", "10", "10");

        planoCobrancaPage.SubmeterFormulario();

        var elemento = planoCobrancaPage.Erros["GrupoVeiculosId"];

        Assert.AreEqual("O grupo de veículos é obrigatório", elemento.Text);
        Assert.IsTrue(elemento.Displayed);
    }

}