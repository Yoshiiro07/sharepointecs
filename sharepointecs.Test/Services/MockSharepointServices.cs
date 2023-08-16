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
    public class MockSharepointServices
    {
        [Fact]
        public void ExtractPage_OK()
        {
            //Arrange
            Mock<IConfiguration> mockIConfiguration = new Mock<IConfiguration>();
            SharepointServices sharepointServices = new SharepointServices(mockIConfiguration.Object);
            var token = sharepointServices.GetAccessToken();

            //Act
            var spModel = sharepointServices.ExtractPage(token, "/Home.aspx");

            //Assert
            Assert.NotNull(spModel);
        }

        [Fact]
        public void ExtractPage_Fail()
        {
            //Arrange
            Mock<IConfiguration> mockIConfiguration = new Mock<IConfiguration>();
            SharepointServices sharepointServices = new SharepointServices(mockIConfiguration.Object);
            var token = sharepointServices.GetAccessToken();

            //Act
            var spModel = sharepointServices.ExtractPage(token, "");

            //Assert
            Assert.Null(spModel);
        }
    }
}
