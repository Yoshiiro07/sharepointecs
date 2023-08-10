using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sharepointecs.Models;

namespace sharepointecs.Services
{
    public interface IControlDBRepository
    {
        public IEnumerable<TBControl> GetListControlDB();

        public bool UpdateChanges(TBControl content);

        public TBControl GetItemControl(SPModel spmodel);

        public TBControl RefreshItem(SPModel spmodel, TBControl tbcontrol);
    }
}
