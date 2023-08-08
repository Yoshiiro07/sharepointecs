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
        public Task<IEnumerable<ControlDBModel>> GetListControlDB();

        public Task<bool> UpdateChangesAsync(SPModel content);
    }
}
