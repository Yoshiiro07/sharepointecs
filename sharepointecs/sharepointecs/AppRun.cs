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
        private readonly ISharepointServices _sharepointService;
        //private readonly IControlDBRepository _controlDBRepository;
        private readonly IConfiguration _configuration;
        private readonly IFileGenerator _fileGenerator;
        private readonly ILogApplication _logApp;
        
        public AppRun(ISharepointServices sharepointService,  IFileGenerator fileGenerator, IConfiguration configuration, ILogApplication logApp)
        {
            _sharepointService = sharepointService;
            //_controlDBRepository = controlDBRepository;
            _configuration = configuration;
            _fileGenerator = fileGenerator;
            _logApp = logApp;
        }

        public void Run()
        {
            _logApp.Log("Começando...");
            var getTokenSp = _sharepointService.GetAccessToken();
            if (getTokenSp == null) { _logApp.Log("Não foi possível obter token"); }
        }

        public void WithControl()
        {
            //try
            //{
            //    _logApp.Log("Iniciando a operação!");
            //    //Check Pages Status
            //    var pagesFromDB = _controlDBRepository.GetListControlDB();
            //    if (pagesFromDB.Count() > 0)
            //    {
            //        var getTokenSp = _sharepointService.GetAccessToken();
            //        if (getTokenSp == null) { _logApp.Log("Não foi possível obter token"); }
            //        foreach (TBControl ctmodel in pagesFromDB)
            //        {
            //            _logApp.Log("Extraindo dados pagina:" + ctmodel.NOM_PAGI);
            //            var content = _sharepointService.ExtractPage(getTokenSp, ctmodel.NOM_PAGI);
            //            if (content != null)
            //            {
            //                DateTime dtModif = Convert.ToDateTime(content.Modified);
            //                if ((ctmodel.DAT_ALTE_PAGI != dtModif))
            //                {
            //                    _logApp.Log("Atualizando tabela de controle");
            //                    var tbc = _controlDBRepository.GetItemControl(content);
            //                    var tbUpdated = _controlDBRepository.RefreshItem(content, tbc);
            //                    _controlDBRepository.UpdateChanges(tbUpdated);
            //                    _logApp.Log("Gerando arquivo");
            //                    var file = _fileGenerator.MakeAWSFile(content);

            //                }
            //                else
            //                {
            //                    _logApp.Log("Sem novas informações para:" + ctmodel.NOM_PAGI);
            //                    TBControl tbc = _controlDBRepository.GetItemControl(content);
            //                    _controlDBRepository.UpdateChanges(tbc);
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        _logApp.Log("Não foram encontrados itens na tabela de controle");
            //        return;
            //    }
            //}

            //catch (Exception ex) { }
        }

    }
}
