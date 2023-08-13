using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using sharepointecs.Services;
using sharepointecs.Models;
using System.IO;

namespace sharepointecs.Test.Services
{
    public class MockFileGenerator
    {
        public MockFileGenerator() { }

        [Fact]
        public void MakeFile_OK()
        {
            //Arrange
            Mock<IFileGenerator> fileGenerator = new Mock<IFileGenerator>();

            //Act
            var file = fileGenerator.Setup(x => x.MakeFile(It.IsAny<SPModel>()));

            //Assert
            Assert.NotNull(file);
        }

        [Fact] public void MakeFile_Fail()
        {
            //Arrange
            Mock<IFileGenerator> fileGenerator = new Mock<IFileGenerator>();

            //Act
            var file = fileGenerator.Setup(x => x.MakeFile(It.IsAny<string>);

            //Assert
            Assert.NotNull(file);
        }
    }
}
