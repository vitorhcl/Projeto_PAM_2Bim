using Projeto_PAM_2Bim.Models;
using Projeto_PAM_2Bim.Models.Enuns;
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
    [QueryProperty("ExercicioSelecionadoId", "eId")]
    public class CadastroExercicioViewModel : BaseViewModel
    {
        private ExercicioServices eService;

        private Int32 _id;
        private string _caminhoArquivo;
        private string _enunciado;
        private int? _idAlterMarcada;
        private NivelEnum _nivel;

        public int Id
        { 
            get => _id;
            set
            {
                _id = value;
                OnPropertyChenged();
            }
        }
        public string CaminhoArquivo { get => _caminhoArquivo; set => _caminhoArquivo = value; }
        public string Enunciado { get => _enunciado; set => _enunciado = value; }
        public int? IdAlterMarcada { get => _idAlterMarcada; set => _idAlterMarcada = value; }
        public NivelEnum Nivel { get => _nivel; set => _nivel = value; }

        private ObservableCollection<TipoClasse> _listaTiposClasse;
        public ObservableCollection<TipoClasse> ListaTiposClasse
        {
            get => _listaTiposClasse;
            set
            {
                if (value != null)
                {
                    _listaTiposClasse = value;
                    OnPropertyChenged();
                }
            }
        }

        private string _exercicioSelecionadoId;
        public string ExercicioSelecionadoId
        {
            set
            {
                if (value != null)
                {
                    _exercicioSelecionadoId = value;
                    OnPropertyChenged();
                }
            }
        }

        private TipoClasse _TipoClasseSelecionado;
        public TipoClasse TipoClasseSelecionado
        {
            get => _TipoClasseSelecionado;
            set
            {
                if (value != null)
                {
                    _TipoClasseSelecionado = value;
                    OnPropertyChenged();
                }
            }
        }

        public CadastroExercicioViewModel()
        {
            string token = Preferences.Get("ExercicioToken", string.Empty);
            eService = new ExercicioServices(token);

            _ = ObterClasses();

            SalvarCommand = new Command(async () => { await SalvarExercicio(); });
            CancelarCommand = new Command(async => CancelarCadastro());
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; set; }

        public async Task ObterClasses()
        {
            try
            {
                ListaTiposClasse = new ObservableCollection<TipoClasse>();
                ListaTiposClasse.Add(new TipoClasse() { Id = 1, Descricao = "Facil" });
                ListaTiposClasse.Add(new TipoClasse() { Id = 2, Descricao = "Medio" });
                ListaTiposClasse.Add(new TipoClasse() { Id = 3, Descricao = "Dificil" });
                OnPropertyChenged(nameof(ListaTiposClasse));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", $"{ex.Message} Detalhes: {ex.InnerException}", "Ok");
            }
        }

        public async Task SalvarExercicio()
        {
            try
            {
                Exercicio model = new Exercicio()
                {
                    Id = this.Id,
                    CaminhoArquivo = this.CaminhoArquivo,
                    Enunciado = this.Enunciado,
                    IdAlterMarcada = this.IdAlterMarcada,
                    Nivel = (NivelEnum)TipoClasseSelecionado.Id
                };

                await eService.PostExercicioAsync(model);
                
                await Application.Current.MainPage.DisplayAlert("Mensagem!", "Dados salvos com sucesso!", "Ok");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", $"{ex.Message} Detalhes: {ex.InnerException}", "Ok");
            }
        }

        private async void CancelarCadastro()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async void CarregarExercicio()
        {
            try
            {
                Exercicio e = await eService.GetExercicioAsync(int.Parse(_exercicioSelecionadoId));

                this.Id = e.Id;
                this.CaminhoArquivo = e.CaminhoArquivo;
                this.Enunciado = e.Enunciado;
                this.IdAlterMarcada = e.IdAlterMarcada;
                TipoClasseSelecionado = this.ListaTiposClasse.FirstOrDefault(tClasse => tClasse.Id == (int)e.Nivel);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", $"{ex.Message} Detalhes: {ex.InnerException}", "Ok");
            }
        }
    }
}
