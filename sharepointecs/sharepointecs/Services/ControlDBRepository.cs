using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sharepointecs.DbContexts;
using sharepointecs.Models;

namespace sharepointecs.Services
{
    public class ControlDBRepository : IControlDBRepository
    {
        private readonly ControlDBContext _context;

        public async Task<bool> UpdateChangesAsync(SPModel spmodel)
        {
            ControlDBModel cmodel = new ControlDBModel();
            cmodel.DAT_ALTE_PAGI = Convert.ToDateTime(spmodel.Modified);
            cmodel.DAT_CRIA_PAGI = Convert.ToDateTime(spmodel.Created);
            cmodel.DAT_ULTU_LEIT = DateTime.Now;
            cmodel.COD_IDT_SHRT = new Guid(spmodel.GUID);
            cmodel.COD_STAT_CARG = "1";

            _context.Update(cmodel);
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<IEnumerable<ControlDBModel>> GetListControlDB()
        {
            return await _context.ControlsDB.OrderBy(c => c.NOM_PAGI).ToListAsync();
        }
    }
}
