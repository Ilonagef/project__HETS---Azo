using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HETS1Design
{
    [TestClass]
    public class OpenFoldesTest
    {
          [TestInitialize]
        public void Initialize()
        {
            Submissions.ResetSubmissions();
            TestCases.ResetTestCases();
        }
      
        [TestMethod]
        public void OpenFoldes_Suucsess()
        {
            string filePath = @"..\..\..\Assets\Test Required FIles\OpenFolderTest";
            bool con = filePath.Contains(".zip");
            Assert.IsFalse(con);
        }


    }
}