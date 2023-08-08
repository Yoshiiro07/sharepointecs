using sharepointecs.DbContexts;
using sharepointecs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs.Test.Services
{
    public class MockControlDBRepository
    {
        private readonly ControlDBContext _context;

        [Fact]
        public async void UpdateChangesAsync(SPModel spmodel)
        {
            //Assert
            int saveChanges = 0;
            ControlDBModel cmodel = new ControlDBModel();
            cmodel.DAT_ALTE_PAGI = Convert.ToDateTime(spmodel.Modified);
            cmodel.DAT_CRIA_PAGI = Convert.ToDateTime(spmodel.Created);
            cmodel.DAT_ULTU_LEIT = DateTime.Now;
            cmodel.COD_IDT_SHRT = new Guid(spmodel.GUID);
            cmodel.COD_STAT_CARG = "1";

            //Act
            _context.Update(cmodel);

            //Assert
            Assert.AreEqual(1, saveChanges);
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<IEnumerable<ControlDBModel>> GetListControlDB()
        {
            //Assert
            return await _context.ControlsDB.OrderBy(c => c.NOM_PAGI).ToListAsync();

            //Act

            //Assert
        }
    }
}
