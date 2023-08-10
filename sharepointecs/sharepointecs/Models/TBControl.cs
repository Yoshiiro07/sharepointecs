using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs.Models
{
    public class TBControl
    {
        [Key]
        public int COD_TABE_CTRL {get;set;}

        public string COD_IDT_SHRT { get; set; }

        public DateTime DAT_INCU { get; set; }

        public DateTime DAT_CRIA_PAGI { get; set; }

        public DateTime DAT_ALTE_PAGI { get; set; }

        public string NOM_PAGI { get; set; }

        public int COD_STAT_CARG { get; set; }

        public DateTime DAT_ULTU_LEIT { get; set; }
    }
}
