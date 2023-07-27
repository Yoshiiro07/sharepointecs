using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs.Services
{
    public interface IAWSServices
    {
        Task<bool> UploadFileAsync(IFormFile file);
    }
}
