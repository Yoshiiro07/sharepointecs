using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using sharepointecs.Services;
using sharepointecs.Models;
using System.IO;
using Microsoft.AspNetCore.Http.Internal;
using Amazon.S3;

namespace sharepointecs.Test.Services
{
    public class MockFileGenerator
    {
        public MockFileGenerator() { }

        [Fact]
        public void MakeFile_OK()
        {
            //Arrange
            Mock<FileGenerator> fileGenerator = new Mock<FileGenerator>();

            //Act
            var file = fileGenerator.Setup(x => x.MakeFile(It.IsAny<SPModel>()));

            //Assert
            Assert.NotNull(file);
        }
    }
}
