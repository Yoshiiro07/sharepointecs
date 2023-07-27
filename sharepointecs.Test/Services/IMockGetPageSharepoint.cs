using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs.Test.Services
{
    interface IMockGetPageSharepoint
    {
        Task<string[]> MakeExtract(string requestPage);
    }
}
