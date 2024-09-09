using LocadoraDeVeiculos.Testes.e2e.Compartilhado;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.GrupoVeiculo;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.Veiculo;

namespace LocadoraDeVeiculos.Testes.e2e.Veiculo;

[TestClass]
public class AoInserirVeiculo : TestFixture
{
    FormularioGrupoVeiculoPageObject grupoVeiculoPage;
    FormularioVeiculoPageObject veiculoPageObject;

    public AoInserirVeiculo()
    {
        grupoVeiculoPage = new FormularioGrupoVeiculoPageObject(driver);
        veiculoPageObject = new FormularioVeiculoPageObject(driver);
    }

    [TestMethod]
    public void Dado_info_validas_deve_mostrar_Veiculo_na_listagem()
    {
        grupoVeiculoPage.Visitar();
        string nomeGrupo = "SUV";
        grupoVeiculoPage.PreencherFormulario(nomeGrupo);
        grupoVeiculoPage.SubmeterFormulario();

        veiculoPageObject.Visitar();
        string foto = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"Veiculo\ap.jpg"));
        veiculoPageObject.PreencherFormulario(foto, "Kicks", "Nissan", "Gasolina", "48", nomeGrupo);

        veiculoPageObject.SubmeterFormulario();

        Assert.IsTrue(driver.PageSource.Contains("Kicks"));
    }

    [TestMethod]
    public void Dado_info_invalidas_deve_permanecer_no_formulario_de_Veiculos()
    {
        grupoVeiculoPage.Visitar();
        string nomeGrupo = "SUV";
        grupoVeiculoPage.PreencherFormulario(nomeGrupo);
        grupoVeiculoPage.SubmeterFormulario();

        veiculoPageObject.Visitar();
        string foto = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"Veiculo\ap.jpg"));
        veiculoPageObject.PreencherFormulario(foto, "Kicks", "Nissan", "Gasolina", "48", nomeGrupo);

        veiculoPageObject.SubmeterFormulario();

        Assert.IsTrue(driver.PageSource.Contains("Kicks"));
    }
}