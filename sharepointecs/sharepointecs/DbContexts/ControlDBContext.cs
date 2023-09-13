using Microsoft.EntityFrameworkCore;
using sharepointecs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs.DbContexts
{
    public class ControlDBContext : DbContext
    {
        public DbSet<Reports> Reports { get; set; } = null!;

        public ControlDBContext(DbContextOptions<ControlDBContext> options)
            : base(options)
        {

        }
    }
}
