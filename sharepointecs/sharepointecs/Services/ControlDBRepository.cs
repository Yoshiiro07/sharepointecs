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
        private readonly DbContext _ControlDBContext;

        public async Task<bool> SaveChangesAsync(string[] content)
        {
            ControlDBModel controlPage = new ControlDBModel();
            controlPage.GUID = new Guid(content[1]);
            controlPage.DataInclusao = DateTime.Now;
            controlPage.Data_Criacao_Pagina = Convert.ToDateTime(content[2]);
            controlPage.Data_Modificacao_Pagina = Convert.ToDateTime(content[3]);
            

            return (await _ControlDBContext.SaveChangesAsync() >= 0);
        }

        public async Task<bool> CheckPageControl(string[] content)
        {
            return false;
        }
    }
}
