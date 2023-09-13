using sharepointecs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs.Services
{
    public interface IDBRepository
    {
        public IEnumerable<Reports> GetListControlDB();

        public bool UpdateChanges(TBControl content);

        public TBControl GetItemControl(SPModel spmodel);

        public TBControl RefreshItem(SPModel spmodel, TBControl tbcontrol);
    }
}
