﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.SharePoint.Client;
using sharepointecs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs.Services
{
    public class FileGenerator : IFileGenerator
    {
        private readonly ILogApplication _logApp;

        public FileGenerator(ILogApplication _ILogApplication)
        {
            _logApp = _ILogApplication;
        }

        public FormFile MakeAWSFile(SPModel spModel) {
            try
            {
                string location = System.IO.Path.GetDirectoryName(typeof(Program).Assembly.Location) + 
                    spModel.FileLeafRef.Remove(0, spModel.FileLeafRef.LastIndexOf("/")) + ".txt";

                //Open the File
                StreamWriter sw = new StreamWriter(location, true, Encoding.ASCII);
                sw.WriteLine("FileLeafRef: " + spModel.FileLeafRef.ToString());
                sw.WriteLine("GUID: " + spModel.GUID.ToString());
                sw.WriteLine("UniqueId: " + spModel.UniqueId.ToString());
                sw.WriteLine("LayoutWebpartsContent: " + spModel.LayoutWebpartsContent.ToString());
                sw.WriteLine("CanvasContent1: " + spModel.CanvasContent1.ToString());
                sw.WriteLine("WikiField: " + spModel.WikiField.ToString());
                sw.WriteLine("Created: " + spModel.Created.ToString());
                sw.WriteLine("Modified: " + spModel.Modified.ToString());
                
                //close the file
                sw.Close();

                FormFile file = new FormFile(null, 0, 0, null, "");
                using (var stream = System.IO.File.OpenRead(location))
                {
                    file = new FormFile(stream, 0, stream.Length, null, location)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "application/text"
                    };
                }
                return file;
            }
            catch (Exception ex)
            {
                _logApp.Log("Não foi possível gerar File AWS: " + ex.Message);
                return null;
            }      
        }
    }
}
