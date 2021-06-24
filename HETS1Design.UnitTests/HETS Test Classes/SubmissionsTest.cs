using System;
using System.Data;
using System.IO;
using HETS1Design;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HETS1Design
{
    [TestClass]
    public class SubmissionsTest
    {
        SingleSubmission s1;

        [TestInitialize]
        public void Initialize()
        {
            Submissions.ResetSubmissions();
            TestCases.ResetTestCases();
            s1 = new SingleSubmission("21325");
            s1.AddCode(@"..\..\..\Assets\Test Required FIles\SubmissionsTest\Source.c");
            Submissions.submissions.Add(s1);
        }

        [TestMethod]
        public void ActivateCompilation_Finished()
        {
            System.Windows.Forms.Button b= new System.Windows.Forms.Button();
            var result = Submissions.ActivateCompilation(b);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ActivateExecution_Finished()
        {
            System.Windows.Forms.Button b = new System.Windows.Forms.Button();
            var result = Submissions.ActivateExecution(b);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ActivateGradingFinished()
        {
            int oldCodeWeight = Submissions.codeWeight;
            int oldExeWeight = Submissions.exeWeight;
            int oldcorrentResultsWeight = Submissions.correctResultsWeight;

            Submissions.codeWeight = 100;
            Submissions.exeWeight = 0;
            Submissions.correctResultsWeight = 0;

            Submissions.ActivateGrading();

            Assert.AreEqual(s1.finalGrade, s1.Grading(Submissions.codeWeight, Submissions.exeWeight, Submissions.correctResultsWeight));


            Submissions.codeWeight = oldCodeWeight;
            Submissions.exeWeight = oldExeWeight;
            Submissions.correctResultsWeight = oldcorrentResultsWeight;
        }

        [TestMethod]
        public void SaveDetailedResultTest()
        {
            Submissions.SaveDetailedResults(@"..\..\..\Assets\Test Required FIles\SubmissionsTest\ZipForTest.zip");
            string createdDirectory = @"..\..\..\Assets\Test Required FIles\SubmissionsTest\Detailed Results HETS - Azo";
            bool createdDirectoryExists = Directory.Exists(createdDirectory);
            Assert.IsTrue(createdDirectoryExists);

            string createdFile = createdDirectory + @"\"+Submissions.submissions[0].submitID+".txt"; 
            bool txtFileExists = File.Exists(createdFile);
            Assert.IsTrue(txtFileExists);


        }

        [TestMethod]
        public void ResetSubmissionsTest()
        {
            Assert.AreEqual(1, Submissions.submissions.Count);
            Submissions.ResetSubmissions();
            Assert.AreEqual(0, Submissions.submissions.Count);
        }



        [TestCleanup]
        public void TestCleanup()
        {
            Submissions.ResetSubmissions();
            TestCases.ResetTestCases();
        }

    }
}
