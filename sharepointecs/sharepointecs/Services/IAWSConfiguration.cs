using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace sharepointecs.Services
{
    public interface IAWSConfiguration
    {
        string AwsAccessKey { get; set; }
        string AwsSecretAccessKey { get; set; }
        string AwsSessionToken { get; set; }
        string BucketName { get; set; }
        string Region { get; set; }
    }
}
