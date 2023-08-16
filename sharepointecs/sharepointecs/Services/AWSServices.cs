using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs.Services
{
    public class AWSServices : IAWSServices
    {
        private readonly string _bucketName;
        private readonly IAmazonS3 _awsS3Client;
        private readonly ILogApplication _logApp;

        public AWSServices(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, string region, string bucketName, ILogApplication _ILogApplication)
        {
            _logApp = _ILogApplication;
            _bucketName = bucketName;
            _awsS3Client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, RegionEndpoint.GetBySystemName(region));
        }

        public async Task<bool> UploadFileAsync(IFormFile file)
        {
            try
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = file.FileName,
                        BucketName = _bucketName,
                        ContentType = file.ContentType
                    };

                    var fileTransferUtility = new TransferUtility(_awsS3Client);

                    await fileTransferUtility.UploadAsync(uploadRequest);

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logApp.Log("Erro ao subir no Bucket S3:" + ex.Message);
                return false;
            }
        }
    }
}
