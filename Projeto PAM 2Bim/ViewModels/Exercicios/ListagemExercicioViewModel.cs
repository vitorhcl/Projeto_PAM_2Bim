using Projeto_PAM_2Bim.Models;
using Projeto_PAM_2Bim.Services.Exercicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projeto_PAM_2Bim.ViewModels.Exercicios
{
    public class ListagemExercicioViewModel : BaseViewModel
    {
        private ExercicioServices eService;

        public ObservableCollection<Exercicio> Exercicios { get; set; }

        private Exercicio _exercicioSelecionado;
        public Exercicio ExercicioSelecionado
        {
            get => _exercicioSelecionado;
            set
            {
                if (value != null)
                {
                    _exercicioSelecionado = value;
                    Shell.Current.GoToAsync($"cadExercicioView?pId={_exercicioSelecionado.Id}");
                }
            }
        }

        public ListagemExercicioViewModel()
        {
            string token = Preferences.Get("ExercicioToken", string.Empty);
            eService = new ExercicioServices(token);
            Exercicios = new ObservableCollection<Exercicio>();

            _ = ObterExercicios();

            NovoExercicio = new Command(async () => { await ExibirCadastroExercicio(); });
            RemoverExercicioCommand = new Command<Exercicio>(async (Exercicio e) => { await RemoverExercicio(e); });
        }

        public ICommand NovoExercicio { get; }
        public ICommand RemoverExercicioCommand { get; }

        public async Task ObterExercicios()
        {
            try
            {
                Exercicios = await eService.GetExerciciosAsync();
                OnPropertyChenged(nameof(Exercicios));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", $"{ex.Message} Detalhes: {ex.InnerException}", "Ok");
            }
        }

        public async Task ExibirCadastroExercicio()
        {
            try
            {
                await Shell.Current.GoToAsync("cadExercicioView");
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", $"{ex.Message} Detalhes: {ex.InnerException}", "Ok");
            }
        }

        public async Task RemoverExercicio(Exercicio e)
        {
            try
            {
                if (await Application.Current.MainPage.DisplayAlert("Confirmação", $"Deseja excluir esse exercício?", "Sim", "Não"))
                {
                    await eService.DeleteExercicioAsync(e.Id);

                    await Application.Current.MainPage.DisplayAlert("Mensagem", "Exercício removido com sucesso!", "Ok");
                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", $"{ex.Message} Detalhes: {ex.InnerException}", "Ok");
            }
        }
    }
}
