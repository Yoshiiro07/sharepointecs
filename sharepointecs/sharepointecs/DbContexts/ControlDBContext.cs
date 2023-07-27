using sharepointecs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs.DbContexts
{
    public class ControlDBContext : DbContext
    {
        public DbSet<ControlDBModel> ControlsDB { get; set; } = null!;

        public ControlDBContext(DbContextOptions<ControlDBContext> options) : base(options){

        }

    }
}
