using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HETS1Design
{
    [TestClass]
    public class SingleSubmissionTest
    {
        SingleSubmission s1,s2,s3;


        [TestInitialize]
        public void Initialize()
        {
            Submissions.ResetSubmissions();
            TestCases.ResetTestCases();
            s1 = new SingleSubmission("21325");
            s2 = new SingleSubmission("54324");
            s3 = new SingleSubmission("53252");
            s2.AddCode(@"..\..\..\Assets\Test Required FIles\SingleSubmissionTest\Source1.c");
            s2.AddExe(@"..\..\..\Assets\Test Required FIles\SingleSubmissionTest\Source1.exe");

            s3.AddExe(@"..\..\..\Assets\Test Required FIles\SingleSubmissionTest\Source2.exe");

            bool isTC = false; // TNC
            string input = "Check input"; // This is a TNC and we know these vakues are incorrect output
            string output = "Check output"; // So we know the result will be true
            TestCases.OnAddTestCase(input, output, isTC); // We want the submisstion at least one currect result
            TestCases.OnAddTestCase(input, output, isTC); // We want the submisstion at least one currect result
        }



        [TestMethod]
        public void SingleSubmission_Creates()
        {
            Assert.IsNotNull(s1);
            Assert.IsNotNull(s1.submitID);
        }

        [TestMethod]
        public void AddCode_Success()
        {
            s1.AddCode(@"..\..\..\Assets\Test Required FIles\SingleSubmissionTest\Source.c");
            Assert.AreEqual(@"..\..\..\Assets\Test Required FIles\SingleSubmissionTest\Source.c", s1.codePath);
            Assert.IsTrue(s1.codeExists);
        }

        [TestMethod]
        public void AddExe_Success()
        {
            s1.AddExe(@"..\..\..\Assets\Test Required FIles\SingleSubmissionTest\Source.exe");
            Assert.AreEqual(@"..\..\..\Assets\Test Required FIles\SingleSubmissionTest\Source.exe", s1.exePath);
            Assert.IsTrue(s1.exeExists);
        }

        [TestMethod]
        public void RunSubmittedProgram_AddedTestCasesToList()
        {
            //TODO
        }

        [TestMethod]
        public void CompareResultsToDesiredResults_Test()
        {
            //TODO, Also name of the method needs to be changed!
        }

        [TestMethod]
        public void CompareBothLists_CheckEqual()
        {
            Assert.IsTrue(s1.CompareBothLists());
        }

        [TestMethod]
        public void Grading_Test()
        {
            s1.AddCode(@"..\..\..\Assets\Test Required FIles\SingleSubmissionTest\Source.c");
            s1.AddExe(@"..\..\..\Assets\Test Required FIles\SingleSubmissionTest\Source.exe");

            s1.CompileSubmittedCode();
            s1.RunSubmittedProgram();
            s1.CompareResultsToDesiredResults();
            s1.CalculateFinalGrade(0, 0, 0);
            decimal currentGrade = (decimal)s1.finalGrade;

            decimal desiredGrade =  0;
            Assert.AreEqual(currentGrade, desiredGrade);
      




            s1.CompileSubmittedCode();
            s1.RunSubmittedProgram();
            s1.CompareResultsToDesiredResults();
            decimal newdesiredGrade = 100;
            decimal newcurrentGrade = (decimal)s1.finalGrade;
            newcurrentGrade=s1.Grading(33, 33, 34);
            Assert.AreEqual(newcurrentGrade, newdesiredGrade);
        }

        [TestMethod]
        public void ResultsVsCorrectResults_Test()
        {
            
        }

        [TestMethod]
        public void CorrectResultsPercentage_Test()
        {
            string st = s1.CorrectResultsPercentage();
            Assert.IsTrue(st.Contains("%"));
        }

        [TestMethod]
        public void GetAllSingleSubmissionResults_Test()
        {
            string st = s2.GetAllSingleSubmissionResults();
            Assert.AreEqual("No results.", st);
            TestCases.OnAddTestCase("2", "2", false);
            s2.CompileSubmittedCode();
            s2.RunSubmittedProgram();
            s2.CorrectResultsCount();
            s2.CompareResultsToDesiredResults();
            string st2 = s2.GetAllSingleSubmissionResults();
            Assert.AreNotEqual("No results.", st2);
        }

        [TestMethod]
        public void CorrectResultsCount()
        {
            s3.AddExe(@"..\..\..\Assets\Test Required FIles\SingleSubmissionTest\Source.exe");
            s3.RunSubmittedProgram();
            s3.CompareResultsToDesiredResults();
            int res3 = s3.CorrectResultsCount();
            Assert.AreEqual(2, res3);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Submissions.ResetSubmissions();
            TestCases.ResetTestCases();
        }
    }
}
