using sharepointecs.Models;
using sharepointecs.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace sharepointecs
{
    public class AppRun
    {
        private readonly ISharepointServices _getPageSharepoint;
        private readonly IControlDBRepository _controlDBRepository;
        private readonly IConfiguration _configuration;
        private readonly IFileGenerator _fileGenerator;
        
        public AppRun(ISharepointServices getPageSharepoint, IControlDBRepository controlDBRepository, IFileGenerator fileGenerator, IConfiguration configuration)
        {
            _getPageSharepoint = getPageSharepoint;
            _controlDBRepository = controlDBRepository;
            _configuration = configuration;
            _fileGenerator = fileGenerator;
        }

        public void Run()
        {
            try
            {
                //Check Pages Status
                var pagesFromDB = _controlDBRepository.GetListControlDB();
                if (pagesFromDB.Count() > 0)
                {
                    var getTokenSp = _getPageSharepoint.GetAccessToken();
                    foreach (TBControl ctmodel in pagesFromDB)
                    {
                        var content = _getPageSharepoint.MakeExtract(getTokenSp, ctmodel.NOM_PAGI);
                        if (content != null)
                        {
                            DateTime dtModif = Convert.ToDateTime(content.Modified);
                            if ((ctmodel.DAT_ALTE_PAGI != dtModif))
                            {

                                TBControl tbc = _controlDBRepository.GetItemControl(content);
                                TBControl tbUpdated = _controlDBRepository.RefreshItem(content, tbc);
                                _controlDBRepository.UpdateChanges(tbUpdated);
                                var file = _fileGenerator.MakeFile(content);
                            }
                            else
                            {
                                TBControl tbc = _controlDBRepository.GetItemControl(content);
                                _controlDBRepository.UpdateChanges(tbc);
                            }
                        }
                    }
                }
                else
                {
                    return;                    
                }
            }

            catch(Exception ex){}
        }
    }
}
