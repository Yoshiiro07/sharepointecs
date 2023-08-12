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
using Serilog;

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
                //Set log
                Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();


                Log.Information("Iniciando a operação!");
                //Check Pages Status
                var pagesFromDB = _controlDBRepository.GetListControlDB();
                if (pagesFromDB.Count() > 0)
                {
                    var getTokenSp = _getPageSharepoint.GetAccessToken();
                    if(getTokenSp == null) { Log.Information("Não foi possível obter token"); }
                    foreach (TBControl ctmodel in pagesFromDB)
                    {
                        Log.Information("Extraindo dados pagina:" + ctmodel.NOM_PAGI);
                        var content = _getPageSharepoint.MakeExtract(getTokenSp, ctmodel.NOM_PAGI);
                        if (content != null)
                        {
                            DateTime dtModif = Convert.ToDateTime(content.Modified);
                            if ((ctmodel.DAT_ALTE_PAGI != dtModif))
                            {
                                Log.Information("Atualizando tabela de controle");
                                TBControl tbc = _controlDBRepository.GetItemControl(content);
                                TBControl tbUpdated = _controlDBRepository.RefreshItem(content, tbc);
                                _controlDBRepository.UpdateChanges(tbUpdated);
                                Log.Information("Gerando arquivo");
                                var file = _fileGenerator.MakeFile(content);
                            }
                            else
                            {
                                Log.Information("Sem novas informações para:" + ctmodel.NOM_PAGI);
                                TBControl tbc = _controlDBRepository.GetItemControl(content);
                                _controlDBRepository.UpdateChanges(tbc);
                            }
                        }
                    }
                }
                else
                {
                    Log.Information("Não foram encontrados itens na tabela de controle");
                    return;                    
                }
            }

            catch(Exception ex){}
        }
    }
}
