Passo a passo

	Parte 1
	
		[TestMethod]
		public void Dado_info_validas_deve_apresentar_a_tela_inicial()
		{
			//arrange
			string enderecoBanco = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=LocadoraDeVeiculosOrm;Integrated Security=True;Pooling=True";

			using (SqlConnection conexao = new SqlConnection(enderecoBanco))
			{
				conexao.Open();
				SqlCommand comandoExcluir = conexao.CreateCommand();
				comandoExcluir.CommandText = "DELETE FROM [ASPNETUSERS]";
				comandoExcluir.ExecuteNonQuery();
			}
			
			IWebDriver driver = new ChromeDriver();

			driver.Navigate().GoToUrl("http://localhost:5125/autenticacao/registrar");

			IWebElement inputUsuario = driver.FindElement(By.CssSelector("input#Usuario"));
			inputUsuario.SendKeys("rech");

			IWebElement inputEmail = driver.FindElement(By.CssSelector("input#Email"));
			inputEmail.SendKeys("rech@gmail.com");

			IWebElement inputSenha = driver.FindElement(By.CssSelector("input#Senha"));
			inputSenha.SendKeys("rech@123");

			IWebElement inputConfirmarSenha = driver.FindElement(By.CssSelector("input#ConfirmarSenha"));
			inputConfirmarSenha.SendKeys("rech@123");

			IWebElement botaoRegistrar = driver.FindElement(By.CssSelector(".btn-primary"));

			//action
			botaoRegistrar.Click();

			//assert
			Assert.IsTrue(driver.PageSource.Contains("Página Inicial"));
			Assert.IsTrue(driver.PageSource.Contains("Seja bem-vindo(a)!"));

			Thread.Sleep(2000);

			driver.Quit();
		}

		[TestMethod]
		public void Dado_info_invalidas_deve_permanecer_na_tela_de_registro()
		{
			//arrange
			string enderecoBanco = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=LocadoraDeVeiculosOrm;Integrated Security=True;Pooling=True";

			using (SqlConnection conexao = new SqlConnection(enderecoBanco))
			{
				conexao.Open();
				SqlCommand comandoExcluir = conexao.CreateCommand();
				comandoExcluir.CommandText = "DELETE FROM [ASPNETUSERS]";
				comandoExcluir.ExecuteNonQuery();
			}

			IWebDriver driver = new ChromeDriver();

			driver.Navigate().GoToUrl("http://localhost:5125/autenticacao/registrar");

			IWebElement inputUsuario = driver.FindElement(By.CssSelector("input#Usuario"));
			inputUsuario.SendKeys("");

			IWebElement inputEmail = driver.FindElement(By.CssSelector("input#Email"));
			inputEmail.SendKeys("rech@gmail.com");

			IWebElement inputSenha = driver.FindElement(By.CssSelector("input#Senha"));
			inputSenha.SendKeys("rech@123");

			IWebElement inputConfirmarSenha = driver.FindElement(By.CssSelector("input#ConfirmarSenha"));
			inputConfirmarSenha.SendKeys("rech@123");

			IWebElement botaoRegistrar = driver.FindElement(By.CssSelector(".btn-primary"));

			//action
			botaoRegistrar.Click();

			//assert

			IWebElement spanErro = driver.FindElement(By.CssSelector("span.field-validation-error[data-valmsg-for='Usuario']"));

			Assert.AreEqual("O usuário é obrigatório", spanErro.Text);

			Assert.IsTrue(driver.PageSource.Contains("Registro de Usuário"));

			Thread.Sleep(2000);

			driver.Quit();
		}
		
	Parte 2
	
		using Microsoft.Data.SqlClient;
		using OpenQA.Selenium;
		using OpenQA.Selenium.Chrome;

		namespace TestProject1;

		[TestClass]
		public class AoInserirGrupoVeiculo
		{
			private static ChromeDriver driver;

			[ClassInitialize]
			public static void ClassInitialize(TestContext ctx)
			{
				driver = new ChromeDriver();
			}

			[TestInitialize]
			public void TestInitialize()
			{
				string enderecoBanco = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=LocadoraDeVeiculosOrm;Integrated Security=True;Pooling=True";

				using (SqlConnection conexao = new SqlConnection(enderecoBanco))
				{
					conexao.Open();
					SqlCommand comandoExcluir = conexao.CreateCommand();
					comandoExcluir.CommandText = "DELETE FROM [ASPNETUSERS]";
					comandoExcluir.ExecuteNonQuery();
				}
			}

			[TestMethod]
			public void Dado_info_validas_deve_apresentar_GrupoVeiculo_na_listagem()
			{
				//arrange
				driver.Navigate().GoToUrl("http://localhost:5125/autenticacao/registrar");

				IWebElement inputUsuario = driver.FindElement(By.CssSelector("input#Usuario"));
				inputUsuario.SendKeys("rech");

				IWebElement inputEmail = driver.FindElement(By.CssSelector("input#Email"));
				inputEmail.SendKeys("rech@gmail.com");

				IWebElement inputSenha = driver.FindElement(By.CssSelector("input#Senha"));
				inputSenha.SendKeys("rech@123");

				IWebElement inputConfirmarSenha = driver.FindElement(By.CssSelector("input#ConfirmarSenha"));
				inputConfirmarSenha.SendKeys("rech@123");

				IWebElement botaoRegistrar = driver.FindElement(By.CssSelector(".btn-primary"));

				botaoRegistrar.Click();

				driver.Navigate().GoToUrl("http://localhost:5125/GrupoVeiculos/inserir");

				IWebElement inputNome = driver.FindElement(By.CssSelector("input#Nome"));
				inputNome.Clear();
				inputNome.SendKeys("Esportivo");

				IWebElement botaoGravar = driver.FindElement(By.CssSelector(".btn-primary"));

				//action
				botaoGravar.Click();

				//assert
				Assert.IsTrue(driver.PageSource.Contains("Listagem de Grupos de Veículos"));
				Assert.IsTrue(driver.PageSource.Contains("Esportivo"));
			}

			[ClassCleanup]
			public static void TestCleanup()
			{
				driver.Quit();
			}
		}
		
	Parte 3
		
		using Microsoft.Data.SqlClient;
		using OpenQA.Selenium.Chrome;

		namespace TestProject1;

		[TestClass]
		public class TestFixture 
		{
			protected static ChromeDriver driver;

			[AssemblyInitialize]
			public static void ClassInitialize(TestContext ctx)
			{
				driver = new ChromeDriver();
			}

			[TestInitialize]
			public void TestInitialize()
			{
				string enderecoBanco = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=LocadoraDeVeiculosOrm;Integrated Security=True;Pooling=True";

				using (SqlConnection conexao = new SqlConnection(enderecoBanco))
				{
					conexao.Open();
					SqlCommand comandoExcluir = conexao.CreateCommand();
					comandoExcluir.CommandText = "DELETE FROM [TBGRUPOVEICULOS]; DELETE FROM [ASPNETUSERS]";
					comandoExcluir.ExecuteNonQuery();
				}
			}

			[AssemblyCleanup]
			public static void TestCleanup()
			{
				driver.Quit();
			}
		}