using Projeto_PAM_2Bim.ViewModels.Exercicios;

namespace Projeto_PAM_2Bim.Views.Exercicio;

public partial class CadastroView : ContentPage
{
	private CadastroExercicioViewModel cadViewModel;

	public CadastroView()
	{
		InitializeComponent();

		cadViewModel = new CadastroExercicioViewModel();
		BindingContext = cadViewModel;
		Title = "Novo Exercíco";
	}
}