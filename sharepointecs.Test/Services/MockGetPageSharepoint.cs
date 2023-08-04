using sharepointecs.Models;
using Microsoft.Identity.Client;
using Microsoft.SharePoint.Client;
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
using sharepointecs.Services;
using Moq;

namespace sharepointecs.Test.Services
{
    public class MockGetPageSharepoint
    {
        private readonly Mock<IConfiguration> _configuration;

        public MockGetPageSharepoint(Mock<IConfiguration> configuration)
        {
            _configuration = configuration;
        }

        [Fact]
        public void MakeExtract_Return_OK()
        {
            // Arrange
            Moq.Mock<IMockGetPageSharepoint> mock = new Moq.Mock<IMockGetPageSharepoint>();

            //Act
            mock.Setup(x => x.MakeExtract(It.IsAny<string>()));

            //Assert
            Assert.NotNull(mock.Object);
        }


        [Fact]
        public void MakeExtract_Return_Not_OK()
        {
            // Arrange
            Moq.Mock<IMockGetPageSharepoint> mock = new Moq.Mock<IMockGetPageSharepoint>();

            //Act
            mock.Setup(x => x.MakeExtract(It.IsAny<string>()));

            //Assert
            Assert.Null(mock.Object);
        }
    }
}
