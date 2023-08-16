using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sharepointecs.DbContexts;
using sharepointecs.Models;
using Microsoft.ProjectServer.Client;
using Microsoft.Extensions.Logging;

namespace sharepointecs.Services
{
    public class ControlDBRepository : IControlDBRepository
    {
        private readonly ControlDBContext _context;

        public ControlDBRepository(ControlDBContext context)
        {
            _context = context;
        }

        public IEnumerable<TBControl> GetListControlDB()
        {
            return _context.tbControl.OrderBy(c => c.NOM_PAGI).Where(x => x.COD_STAT_CARG == 1).ToList();
        }

        public bool UpdateChanges(TBControl tbcontrol)
        {
            _context.Update(tbcontrol);
            return (_context.SaveChanges() > 0);
        }

        public TBControl GetItemControl(SPModel spmodel)
        {
            TBControl tb = _context.tbControl.FirstOrDefault(x => x.COD_IDT_SHRT == spmodel.GUID);
            if(tb != null) { return tb; } else { return null; }
        }

        public TBControl RefreshItem(SPModel spmodel, TBControl tbcontrol)
        {
            tbcontrol.DAT_ULTU_LEIT = DateTime.Now;
            tbcontrol.COD_STAT_CARG = 1;
            tbcontrol.DAT_ALTE_PAGI = Convert.ToDateTime(spmodel.Modified);
            return tbcontrol;
        }
    }
}
