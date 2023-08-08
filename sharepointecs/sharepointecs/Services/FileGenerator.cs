using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using sharepointecs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace sharepointecs.Services
{
    public class FileGenerator : IFileGenerator
    {
        public FileGenerator()
        {

        }

        FormFile MakeFile(SPModel spModel) {
            try
            {
                var location = System.IO.Path.GetDirectoryName(typeof(Program).Assembly.Location);

                //Open the File
                StreamWriter sw = new StreamWriter(location + "file.txt", true, Encoding.ASCII);
                sw.WriteLine("FileLeafRef: " + spModel.FileLeafRef.ToString());
                sw.WriteLine("GUID: " + spModel.GUID.ToString());
                sw.WriteLine("UniqueId: " + spModel.UniqueId.ToString());
                sw.WriteLine("LayoutWebpartsContent: " + spModel.LayoutWebpartsContent.ToString());
                sw.WriteLine("CanvasContent1: " + spModel.CanvasContent1.ToString());
                sw.WriteLine("ContentType: " + spModel.ContentType.ToString());
                sw.WriteLine("Created: " + spModel.Created.ToString());
                sw.WriteLine("Modified: " + spModel.Modified.ToString());
                sw.WriteLine("WikiField: " + spModel.WikiField.ToString());

                //close the file
                sw.Close();

                var file = new FormFile(null, 0, 0, null, "");
                string path = location + "file.txt";
                using (var stream = File.OpenRead(path))
                {
                    file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "application/pdf"
                    };                 
                }
                return file;
            }
            catch (Exception ex)
            {
                return new FormFile(null, 0, 0, null, "");
            }      
        }
    }
}
