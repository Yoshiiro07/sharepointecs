using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.SharePoint.Client;
using Microsoft.Extensions.Configuration;

namespace sharepointecs.Services
{
    public class AWSConfiguration : IAWSConfiguration
    {
        public AWSConfiguration()
        {
            BucketName = "";
            Region = "";
            AwsAccessKey = "";
            AwsSecretAccessKey = "";
            AwsSessionToken = "";
        }

        public string BucketName { get; set; }
        public string Region { get; set; }
        public string AwsAccessKey { get; set; }
        public string AwsSecretAccessKey { get; set; }
        public string AwsSessionToken { get; set; }
    }
}
