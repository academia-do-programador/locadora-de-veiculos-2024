using LocadoraDeVeiculos.Testes.Interface.Compartilhado;
using LocadoraDeVeiculos.Testes.Interface.Compartilhado.PageObjects;

namespace LocadoraDeVeiculos.Testes.Interface.Autenticacao;

[TestClass]
public class AoRegistrarUsuario : TestFixture
{
    private RegistrarUsuarioPageObject registroUsuarioPage;

    public AoRegistrarUsuario()
    {
        registroUsuarioPage = new RegistrarUsuarioPageObject(driver);
    }

    [TestMethod]
    public void Dado_info_validas_deve_apresentar_a_tela_inicial() //cenário feliz, cenário principal
    {
        //arrange
        registroUsuarioPage.Visitar();
        registroUsuarioPage.PreecherFormulario("rech2", "rech2@gmail.com", "123", "123");

        //action
        registroUsuarioPage.SubmeterFormulario();

        //assert
        Assert.IsTrue(driver.PageSource.Contains("Página Inicial"));
        Assert.IsTrue(driver.PageSource.Contains("Seja bem-vindo(a)!"));
    }

    [TestMethod]
    public void Dado_info_invalidas_deve_permanecer_na_tela_de_registro() //cenário alternativo 01
    {
        //arrange
        registroUsuarioPage.Visitar();
        registroUsuarioPage.PreecherFormulario("", "rech@gmail.com", "123", "123");

        //action
        registroUsuarioPage.SubmeterFormulario();

        //assert
        Assert.IsTrue(driver.PageSource.Contains("Registro de Usuário"));
        Assert.IsTrue(driver.PageSource.Contains("O usuário é obrigatório"));
    }
}