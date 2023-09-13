using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs.Models
{
    public class Reports
    {
        [Key]
        public int ReportID { get; set; }

        public string Descricao { get; set; }

        public string Tipo { get; set; }

        public string Situacao { get; set; }

    }
}
