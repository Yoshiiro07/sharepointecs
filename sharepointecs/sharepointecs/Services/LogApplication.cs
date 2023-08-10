using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs.Services
{
    public class LogApplication : ILogApplication
    {
        public void Log(string message)
        {
            //Log.Logger = new LoggerConfiguration()
            //     .WriteTo.Console()
            //     .CreateLogger();
        }
    }
}
