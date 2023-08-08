using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using sharepointecs.Services;
using sharepointecs.Models;
using Microsoft.Identity.Client;
using Microsoft.SharePoint.Client;

namespace sharepointecs.Test.Services
{
    public class MockGetPageSharepoint
    {
        private readonly IConfiguration _configuration;

        public MockGetPageSharepoint(IConfiguration configuration){
            _configuration = configuration;
        }

        [Fact]
        public void MakeExtract_Return_OK()
        {
            // Arrange
            Mock<IGetPageSharepoint> mock = new Mock<IGetPageSharepoint>();
            //Act
            mock.Setup(x => x.MakeExtract(It.IsAny<string>()));

            Assert.NotNull(mock.Object);
        }

        [Fact]
        private async void MakeExtractWithCertificate_OK()
        {         
            SPModel mockSPmodel = new SPModel();
            Mock<GetPageSharepoint> mockGetPageSharepoint = new Mock<GetPageSharepoint>();

            var tenantName = _configuration.GetValue<string>("SharepointSettings:TenantName");
            var token = await mockGetPageSharepoint.Setup(x => x.GetAccessTokenWithCertificate()).Returns(Task.FromResult());
            var siteUrl = $"https://{tenantName}.sharepoint.com/";

            using (var context = new ClientContext(siteUrl))
            {
                context.ExecutingWebRequest += (s, e) =>
                {
                    e.WebRequestExecutor.RequestHeaders["Authorization"] =
                        "Bearer " + token;
                };

                var web = context.Web;
                context.Load(web);
                context.ExecuteQuery();
                mockSPmodel.FileLeafRef = web.Title;
            }

            Assert.NotNull(mockSPmodel);
        }

        [Fact]
        private async void MakeExtractWithCertificate_Error()
        {
            SPModel mockSPmodel = new SPModel();
            Mock<GetPageSharepoint> mockGetPageSharepoint = new Mock<GetPageSharepoint>();

            var tenantName = _configuration.GetValue<string>("SharepointSettings:TenantName");
            var token = await mockGetPageSharepoint.Setup(x => x.GetAccessTokenWithCertificate()).Returns(Task.FromResult());
            var siteUrl = $"https://{tenantName}.sharepoint.com/";

            using (var context = new ClientContext(siteUrl))
            {
                context.ExecutingWebRequest += (s, e) =>
                {
                    e.WebRequestExecutor.RequestHeaders["Authorization"] =
                        "Bearer " + token;
                };

                var web = context.Web;
                context.Load(web);
                context.ExecuteQuery();
                mockSPmodel.FileLeafRef = web.Title;
            }

            Assert.Null(mockSPmodel);
        }
    }
}
