using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using sharepointecs.Services;
using sharepointecs.Models;
using Microsoft.Identity.Client;
using Microsoft.SharePoint.Client;

namespace sharepointecs.Test.Services
{
    public class MockGetPageSharepoint
    {
        private readonly IConfiguration _configuration;

        public MockGetPageSharepoint(IConfiguration configuration){
            _configuration = configuration;
        }

        
    }
}
