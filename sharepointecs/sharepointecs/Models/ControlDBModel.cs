using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs.Models
{
    public class ControlDBModel
    {
        public ControlDBModel() { }

        public int ID {get;set;}

        public Guid GUID { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime Data_Criacao_Pagina { get; set; }

        public DateTime Data_Modificacao_Pagina { get; set; }

        public string URL { get; set; }

        public string NomePagina { get; set; }
    }
}
