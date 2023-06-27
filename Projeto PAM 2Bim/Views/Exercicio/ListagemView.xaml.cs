using Projeto_PAM_2Bim.ViewModels.Exercicios;

namespace Projeto_PAM_2Bim.Views.Exercicio;

public partial class ListagemView : ContentPage
{
	private ListagemExercicioViewModel viewModel;

	public ListagemView()
	{
		InitializeComponent();

		viewModel = new ListagemExercicioViewModel();
		BindingContext = viewModel;
		Title = "Exercícios";
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_ = viewModel.ObterExercicios();
	}
}