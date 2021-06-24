using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Windows.Forms;

//namespace HETS1Design
//{
//    [TestClass]
//    public class AUseScript
//    {
//        [TestInitialize]
//        public void Init()
//        {
//            Submissions.ResetSubmissions();
//            TestCases.ResetTestCases();
//        }

//        [TestMethod]
//        public void TestAFullUsageGoodCase()
//        {
//            //1. Extract submissions from zip.
//            string zip = @"..\..\..\Assets\Test Required FIles\AUseScript\Moodle Submissions.zip";
//            ZipArchiveHandler.GetSubmissionData(zip, true);

//            //2. Get test cases from i/o files.
//            string intputFile = @"..\..\..\Assets\Test Required FIles\AUseScript\InputTestCasesGoodExample.txt";
//            string outputFile = @"..\..\..\Assets\Test Required FIles\AUseScript\OutputTestCasesGoodExample.txt";
//            TestCases.ExtractTestCasesFromText(intputFile, outputFile);

//            //3. Activate compilation (this require a button for updating its text)
//            Submissions.ActivateCompilation(new Button());
//            Submissions.ActivateExecution(new Button());

//            //4. Activate grading
//            Submissions.ActivateGrading();

//            //5. Save the results
//            Submissions.SaveDetailedResults(zip);
//        }
//    }
//}
