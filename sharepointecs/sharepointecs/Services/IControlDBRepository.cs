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
        Task<bool> SaveChangesAsync(string[] content);
    }
}
