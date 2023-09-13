using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sharepointecs.Models;
using sharepointecs.DbContexts;

namespace sharepointecs.Services
{
    public class DBRepository : IDBRepository
    {
        private readonly ControlDBContext _dbcontext;

        public DBRepository(ControlDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public TBControl GetItemControl(SPModel spmodel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reports> GetListControlDB()
        {
            return _dbcontext.Reports.Where(x => x.ReportID > 0).ToList();
        }

        public TBControl RefreshItem(SPModel spmodel, TBControl tbcontrol)
        {
            throw new NotImplementedException();
        }

        public bool UpdateChanges(TBControl content)
        {
            throw new NotImplementedException();
        }
    }
}
