using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;


namespace HETS1Design
{
    public static class Submissions
    {
        public static List<SingleSubmission> submissions = new List<SingleSubmission>(); //List of submissions from students.
        public static int codeWeight = -1, exeWeight = -1, correctResultsWeight = -1;
        public static bool checkCode = true; //Default at start.
        public static bool checkExe = true;       


        //Activates compilation for all submissions.
        public static bool ActivateCompilation(Button btnCompile)
        {
            int currentlyCompiling = 1;
            foreach (SingleSubmission sub in submissions)
            {
                btnCompile.Text = "Compiling Code..." + currentlyCompiling.ToString() + "/" + submissions.Count.ToString();
                btnCompile.Update();

                sub.CompileSubmittedCode(); //The actual compilation of code.

                currentlyCompiling++;
            }
            return true;
        }


        //Activates all the .exe files in the submissions. We wanted to avoid using an extra event, so we passed the button to update.
        public static bool ActivateExecution(Button btnRun)
        {
            int currentlyRunning = 1;
            foreach (SingleSubmission sub in submissions)
            {
                btnRun.Text = "Running Programs... " + currentlyRunning.ToString() + "/" + submissions.Count.ToString();
                btnRun.Update(); //Update button text for currently running program.

                sub.RunSubmittedProgram(); //The actual running of program.
                sub.CompareResultsToDesiredResults();

                currentlyRunning ++;
            }
            return true;
        }

        //Activates grading for all of the submissions.
        public static void ActivateGrading()
        {
            if (codeWeight != -1) //It can be -1 only when grading is turned off.
            {
                foreach (SingleSubmission sub in submissions)
                {
                    sub.CalculateFinalGrade(codeWeight,exeWeight, correctResultsWeight);
                }
            }
        }

        //Clears the submissions list.
        public static void ResetSubmissions()
        {
            submissions.Clear();
        }
        


        /*This is not a very testable method since it's mainly working with DataTables and those require
         alot of "hard coding" in many cases and to test it we'll need to have another buildup of a long
         test because of the very same DataTable cration. Thus, we exclude this from code coverage.*/
        [ExcludeFromCodeCoverage]
        //Saves the .csv table in the desired location.
        public static DataTable GetResultsTable()
        {
            ActivateGrading();

            string compiler = "Compiler version is 64Bit";
            if (CodeChecker.use32bitCompiler)
                compiler = "Compiler version is 32Bit";
            DataTable rdt=new DataTable("Submissions Results ("+compiler+"):");


            //Name the headers.
            rdt.Columns.Add("ID");
            rdt.Columns.Add(".c File Submitted");
            rdt.Columns.Add(".c File Compiled");
            rdt.Columns.Add(".exe File Submitted");
            rdt.Columns.Add("Success Rate");
            rdt.Columns.Add("Grade");
            rdt.Columns.Add("Possible Cheating");



            foreach (SingleSubmission sub in Submissions.submissions)
            {
                DataRow submissionRow = rdt.NewRow();
                string whatToWrite="";

                submissionRow["ID"] = sub.submitID;

                if (sub.codeExists)
                    whatToWrite ="Yes";
                else
                    whatToWrite = "No";
                submissionRow[".c File Submitted"] = whatToWrite;


                if (sub.compiledExePath != null) //Making sure there's a correct path in compiled .exe path (there's no path <2).
                    whatToWrite = "Yes";
                else
                    whatToWrite = "No";
                submissionRow[".c File Compiled"] = whatToWrite;



                if (sub.exePath != null) //Making sure there's a correct path in submitted .exe path (there's no path <2).
                    whatToWrite = "Yes";
                else
                    whatToWrite = "No";
                submissionRow[".exe File Submitted"] = whatToWrite;


                
                submissionRow["Success Rate"] = sub.CorrectResultsPercentage();


                if (codeWeight == -1)
                    whatToWrite = "N/A";
                else
                    whatToWrite = sub.finalGrade.ToString();
                submissionRow["Grade"] = whatToWrite;


                if (sub.possibleCheating)
                    whatToWrite = "Yes";
                else
                    whatToWrite = "";
                submissionRow["Possible Cheating"] = whatToWrite;                



                rdt.Rows.Add(submissionRow);
            }

            return rdt;
        }




        //Returns a text with the detailed results.
        public static void SaveDetailedResults(string zipPath) //We may turn this into the final csv file at some point.
        {
            if (!Directory.Exists(Path.GetDirectoryName(zipPath) + @"\Detailed Results HETS - Azo"))
            Directory.CreateDirectory(Path.GetDirectoryName(zipPath) + @"\Detailed Results HETS - Azo");


            ActivateGrading();

            
            foreach (SingleSubmission sub in Submissions.submissions)
            {
                string createText = "Compiler version: 64Bit\r\n\r\n";
                if (CodeChecker.use32bitCompiler)
                    createText = "Compiler version: 32Bit\r\n\r\n";

                createText +="ID: " + sub.submitID + "\r\n"
                    + "Code path: " + sub.codePath + "\r\n"
                    + "Exe path: " + sub.exePath + "\r\n"
                    + "Code submitted: " + sub.codeExists + "\r\n"
                    + "Exe submitted: " + sub.exeExists + "\r\n"
                    + "Compiler output: " + sub.compilerOutput + "\r\n"
                    + "Compiled Exe path: " + sub.compiledExePath + "\r\n\r\n"
                    + sub.GetAllSingleSubmissionResults();

                createText += "\r\nOverall success rate of: " + sub.CorrectResultsPercentage();
                if (sub.possibleCheating)
                    createText += " with POSSIBLE CHEATING!\r\n\r\n";
                else
                    createText += "\r\n\r\n";

                if (codeWeight!=-1)
                createText += "Grade: " + sub.finalGrade.ToString() + "\r\n\r\n";

                File.WriteAllText(Path.GetDirectoryName(zipPath) + @"\Detailed Results HETS - Azo\" + sub.submitID + ".txt", createText);


            }
        }
    }
}
