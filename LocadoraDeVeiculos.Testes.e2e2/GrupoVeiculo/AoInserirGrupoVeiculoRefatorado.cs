using LocadoraDeVeiculos.Testes.e2e.Compartilhado;
using LocadoraDeVeiculos.Testes.e2e.PageObjects;

namespace LocadoraDeVeiculos.Testes.e2e.GrupoVeiculo;

[TestClass]
public class AoInserirGrupoVeiculoRefatorado : TextFixture
{
    FormularioGrupoVeiculoPageObject grupoVeiculoPage;

    public AoInserirGrupoVeiculoRefatorado()
    {
        grupoVeiculoPage = new FormularioGrupoVeiculoPageObject(driver);
    }

    [TestMethod]
    [DataRow("Carros de Luxo")]
    public void Dado_info_validas_deve_mostrar_grupo_na_listagem(string nomeGrupo)
    {
        grupoVeiculoPage.Visitar();
        grupoVeiculoPage.PreencherFormulario(nomeGrupo);

        grupoVeiculoPage.SubmeterFormulario();

        Assert.IsTrue(driver.PageSource.Contains(nomeGrupo));
        grupoVeiculoPage.ExcluirRegistro(nomeGrupo);
    }

    [TestMethod]
    [DataRow("", "O nome é obrigatório")]
    [DataRow("a", "O nome deve conter ao menos 3 caracteres")]
    public void Dado_info_invalidas_deve_permanecer_na_pagina(string nomeGrupo, string mensagemErro)
    {
        grupoVeiculoPage.Visitar();
        grupoVeiculoPage.PreencherFormulario(nomeGrupo);

        grupoVeiculoPage.SubmeterFormulario();

        var elemento = grupoVeiculoPage.Erros["Nome"];

        Assert.AreEqual(mensagemErro, elemento.Text);
        Assert.IsTrue(elemento.Displayed);
    }
}