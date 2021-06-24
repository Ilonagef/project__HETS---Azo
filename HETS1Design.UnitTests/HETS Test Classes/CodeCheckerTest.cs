using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HETS1Design
{
    [TestClass]
    public class CodeCheckerTest
    {

        [TestMethod]
        public void CompileCode_ReturnsOutput()
        {
            //Arrange
            var filePath = @"..\..\..\Assets\Test Required FIles\CodeCheckerTest\Source.c";
            //Act
            var results = CodeChecker.CompileCode(filePath);
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void RunEXE_ReturnsOutput()
        {
            //Arrange
            var filePath = @"..\..\..\Assets\Test Required FIles\CodeCheckerTest\Source.exe";
            var input = "2 3";
            //Act
            var results = CodeChecker.RunEXE(filePath, input);
            Assert.IsNotNull(results);
        }


    }
}
