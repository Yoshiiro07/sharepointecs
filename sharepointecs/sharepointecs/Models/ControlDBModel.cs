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

        public int COD_TABE_CTRL {get;set;}

        public Guid COD_IDT_SHRT { get; set; }

        public DateTime DAT_INCU { get; set; }

        public DateTime DAT_CRIA_PAGI { get; set; }

        public DateTime DAT_ALTE_PAGI { get; set; }

        public string NOM_PAGI { get; set; }

        public string COD_STAT_CARG { get; set; }

        public DateTime DAT_ULTU_LEIT { get; set; }
    }
}
