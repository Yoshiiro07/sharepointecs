using sharepointecs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs.Services
{
    public interface ISharepointServices
    {
        string GetAccessToken();
        SPModel MakeExtract(string token, string requestPage);
    }
}
