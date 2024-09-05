using LocadoraDeVeiculos.Testes.e2e.Compartilhado;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.GrupoVeiculo;

namespace LocadoraDeVeiculos.Testes.e2e.GrupoVeiculo;

[TestClass]
public class AoEditarGrupoVeiculo : TextFixture
{
    FormularioGrupoVeiculoPageObject grupoVeiculoPage;

    public AoEditarGrupoVeiculo()
    {
        grupoVeiculoPage = new FormularioGrupoVeiculoPageObject(driver);
    }

    [TestMethod]
    [DataRow("Carros de Luxo", "Carros Esportivos")]
    public void Dado_info_validas_deve_mostrar_grupo_atualizado_na_listagem(string nomeGrupo, string nomeGrupoAtualizado)
    {
        grupoVeiculoPage.Visitar();
        grupoVeiculoPage.PreencherFormulario(nomeGrupo);
        grupoVeiculoPage.SubmeterFormulario();

        var id = grupoVeiculoPage.GetId(nomeGrupo);

        grupoVeiculoPage.Visitar(id);
        grupoVeiculoPage.PreencherFormulario(nomeGrupoAtualizado);
        grupoVeiculoPage.SubmeterFormulario();

        Assert.IsTrue(driver.PageSource.Contains(nomeGrupoAtualizado));
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