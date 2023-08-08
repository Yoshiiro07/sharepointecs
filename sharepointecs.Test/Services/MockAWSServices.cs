using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using Amazon.S3;

namespace sharepointecs.Test.Services
{
    public class MockAWSServices
    {

        [Fact]
        public async void UploadFileAsync()
        {
            Mock<IFormFile> file = new Mock<IFormFile>();
            string _bucketName = "";
            IAmazonS3 _awsS3Client = new IAmazonS3();

            try
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.Setup(x => x.CopyTo(newMemoryStream));
                    
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = file.FileName,
                        BucketName = _bucketName,
                        ContentType = file.ContentType
                    };

                    var fileTransferUtility = new TransferUtility((IAmazonS3)_awsS3Client);

                    await fileTransferUtility.UploadAsync(uploadRequest);

                    //Assert
                    Assert.Equal(true, true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
