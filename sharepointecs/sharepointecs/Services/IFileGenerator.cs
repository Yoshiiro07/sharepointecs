using Microsoft.AspNetCore.Http.Internal;
using sharepointecs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs.Services
{
    public interface IFileGenerator
    {
        FormFile MakeFile(SPModel spModel);
    }
}
