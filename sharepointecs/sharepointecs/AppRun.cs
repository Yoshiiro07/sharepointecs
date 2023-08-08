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
using AutoMapper;

namespace sharepointecs
{
    public class AppRun
    {
        private readonly IGetPageSharepoint _getPageSharepoint;
        private readonly IControlDBRepository _controlDBRepository;
        private readonly IConfiguration _configuration;
        private readonly IFileGenerator _fileGenerator;
        private readonly IMapper _mapper;

        public AppRun(IGetPageSharepoint getPageSharepoint, IControlDBRepository controlDBRepository, IFileGenerator fileGenerator, IConfiguration configuration)
        {
            _getPageSharepoint = getPageSharepoint;
            _controlDBRepository = controlDBRepository;
            _configuration = configuration;
            _fileGenerator = fileGenerator;
        }

        public void Run()
        {
            //Check Pages Status
            var pagesFromDB = _controlDBRepository.GetListControlDB();
            IEnumerable<ControlDBModel> pages = _mapper.Map<IEnumerable<ControlDBModel>>(pagesFromDB);

            foreach (ControlDBModel ctmodel in pages) {
                if (ctmodel.DAT_ALTE_PAGI.Day != DateTime.Now.Day)
                {
                    var content = _getPageSharepoint.MakeExtract(ctmodel.NOM_PAGI);
                    if(content != null) { _controlDBRepository.UpdateChangesAsync(_mapper.Map<SPModel>(content)); }
                    var file = _fileGenerator.MakeFile(_mapper.Map<SPModel>(content));
                    
                }
            }
        }
    }
}
