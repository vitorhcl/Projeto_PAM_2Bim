using Projeto_PAM_2Bim.Views.Exercicio;

namespace Projeto_PAM_2Bim;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
		Routing.RegisterRoute("cadExercicioView", typeof(CadastroView));
	}
}
