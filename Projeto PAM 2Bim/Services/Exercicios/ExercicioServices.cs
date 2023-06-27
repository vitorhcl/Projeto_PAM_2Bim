using AppRpgEtec.Services;
using Projeto_PAM_2Bim.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_PAM_2Bim.Services.Exercicios
{
    public class ExercicioServices : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://marshi.somee.com/Projeto_PAM_2Bim/Exercicios";
        private string _token;

        public ExercicioServices(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<ObservableCollection<Exercicio>> GetExerciciosAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Exercicio> listaExercicios = await _request.GetAsync<ObservableCollection<Exercicio>>(apiUrlBase + urlComplementar, _token);
            return listaExercicios;
        }

        public async Task<Exercicio> GetExercicioAsync(int exercicioId)
        {
            string urlComplementar = string.Format("/{0}", exercicioId);
            var exercicio = await _request.GetAsync<Exercicio>(apiUrlBase + urlComplementar, _token);
            return exercicio;
        }

        public async Task<int> PostExercicioAsync(Exercicio e)
        {
            return await _request.PostReturnIntTokenAsync(apiUrlBase, e, _token);
        }

        public async Task<int> PutExercicioAsync(Exercicio e)
        {
            var result = await _request.PutAsync(apiUrlBase, e, _token);
            return result;
        }

        public async Task<int> DeleteExercicioAsync(int exercicioId)
        {
            string urlComplementar = string.Format("/{0}", exercicioId);
            var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
            return result;
        }

        public async Task<int> MarcarAlternativaAsync(int exercicioId, int idAlter)
        {
            string urlComplementar = string.Format("{0}", "/Marcar");
            Exercicio e = new Exercicio();
            e.Id = exercicioId;
            e.IdAlterMarcada = idAlter;

            var result = await _request.PutAsync(apiUrlBase + urlComplementar, e, _token);
            return result;
        }
    }
}
