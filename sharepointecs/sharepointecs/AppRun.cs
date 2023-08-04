using sharepointecs.Models;
using sharepointecs.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs
{
    public class AppRun
    {
        private readonly IGetPageSharepoint _getPageSharepoint;
        private readonly IControlDBRepository _controlDBRepository;

        public AppRun(IGetPageSharepoint getPageSharepoint, IControlDBRepository controlDBRepository)
        {
            _getPageSharepoint = getPageSharepoint;
            _controlDBRepository = controlDBRepository;
        }

        public async void Run()
        {
            var result = false;
            Task handleThing = _getPageSharepoint.MakeExtract("https://3scyh4.sharepoint.com/SitePages/Teste.aspx");
            handleThing.Wait();
        }
    }
}
