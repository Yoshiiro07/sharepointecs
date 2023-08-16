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
using Castle.Core.Configuration;

namespace sharepointecs.Test.Services
{
    public class MockFileGenerator
    {
        [Fact]
        public void MakeLocalFile_OK()
        {
            //Arrange
            Mock<ILogApplication> mockILogApplication = new Mock<ILogApplication>();
            SPModel spmodel = new SPModel();
            FileGenerator fileGen = new FileGenerator(mockILogApplication.Object);
            spmodel.FileLeafRef = "/teste";
            spmodel.GUID = "d0446aae-f944-4a64-a03d-5f0b434396ab";
            spmodel.WikiField = "";
            spmodel.LayoutWebpartsContent = "";
            spmodel.Created = "10/10/2000";
            spmodel.Modified = "10/10/2000";
            spmodel.CanvasContent1 = "";
            spmodel.Title = "";
            spmodel.UniqueId = "";

            //act
            var work = fileGen.MakeAWSFile(spmodel);

            //assert
            Assert.NotNull(work);
        }
        [Fact]
        public void MakeLocalFile_Fail()
        {
            //Arrange
            Mock<ILogApplication> mockILogApplication = new Mock<ILogApplication>();
            Mock<SPModel> mockSPModel = new Mock<SPModel>();
            FileGenerator fileGen = new FileGenerator(mockILogApplication.Object);
            mockSPModel.Object.FileLeafRef = "/teste";

            //act
            var work = fileGen.MakeAWSFile(mockSPModel.Object);

            //assert     
            Assert.Null(work);
        }
    }
}
