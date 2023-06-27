using Projeto_PAM_2Bim.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_PAM_2Bim.Models
{
    public class Exercicio
    {
        public Int32 Id { get; set; }
        public string CaminhoArquivo { get; set; }
        public string Enunciado { get; set; }
        public int? IdAlterMarcada { get; set; }
        public NivelEnum Nivel { get; set; }
    }
}
