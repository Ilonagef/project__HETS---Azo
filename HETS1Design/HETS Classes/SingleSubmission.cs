using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HETS1Design
{
    public class SingleSubmission
    {

        public string submitID { get; private set; } //Submission folder name. 
        public string codePath { get; private set; } //.c file.
        public bool codeExists { get; private set; } 
        public string compilerOutput { get; private set; } //Errors/Warnings from the compiler.
        public string exePath { get; private set; } //.exe file.
        public string compiledExePath { get; private set; } //.exe file made by our compiler.
        public bool exeExists { get; private set; }
        public bool possibleCheating { get; private set; }
        public List<OutputResult> submittedProgramOutputs { get; private set; } //Program output per test case.          
        public List<OutputResult> compiledProgramOutputs { get; private set; } //In case we have 2 exe files, compiled one and attached one
        public int numberOfOverallResults;
        public int finalGrade { get; private set; } //Final grade.

        /*Every submission must have an ID, paths will be added only if an ID exists. 
        (On a new submission creation there's no code/exe files yet)*/
        public SingleSubmission(string submitID) 
        {
            this.submitID = Path.GetFileName(submitID);
            codeExists = false;
            exeExists = false;
            possibleCheating = false;
            submittedProgramOutputs = new List<OutputResult>(); //Program output per test case.          
            compiledProgramOutputs = new List<OutputResult>();
            finalGrade = 0;
        }

        //Add the .c code path.
        public void AddCode(string codePath)
        {
            this.codePath = codePath;
            if (codePath != null)
                codeExists = true;
        }

        //Add the submitted .exe path.
        public void AddExe(string exePath)
        {
            this.exePath = exePath;
            if (exePath != null)
                exeExists = true;
        }
        
               
        //Run this function from Start buttun. Compile the .c code.
        //We don't pass an argument here since SingleSubmission it supposed to be contained in Submissions and it has the list.
        public void CompileSubmittedCode()
        {
            if (codeExists&&Submissions.checkCode)
            {
                this.compilerOutput = CodeChecker.CompileCode(codePath);
                //If it succeeds, the new .exe file path should be this (replace ".c" with ".exe"):
                this.compiledExePath = codePath.Substring(0, codePath.Length - 2) + ".exe";
            }

            if (File.Exists(compiledExePath))
                this.exeExists = true; //If it works, then we have a code.
            else
                this.compiledExePath = null; //If the path doesn't exist (compilation failed), remove it from saved .exe path.
        }


        //Since RunExe returns the results string, we run it and right after add it to the result list.
        public void RunSubmittedProgram()
        {
            if (exeExists)
            {
                if (TestCases.testCases.Count != 0)
                {
                    numberOfOverallResults = 0;
                    submittedProgramOutputs.Clear(); //Reset the output results upon a new Run so
                    compiledProgramOutputs.Clear(); //we can add fresh results.
                    foreach (SingleTestCase tc in TestCases.testCases)
                    {
                        if (File.Exists(exePath)&&Submissions.checkExe) //If there's a submitted .exe and it needs to be checked.
                        {
                            string outputResults = CodeChecker.RunEXE(exePath, tc.input);
                            submittedProgramOutputs.Add(new OutputResult(outputResults));
                        }

                        if (File.Exists(compiledExePath)&&Submissions.checkCode) //If there's just the compiled .exe and only it needs to be checked.
                        {
                            string outputResults = CodeChecker.RunEXE(compiledExePath, tc.input);
                            compiledProgramOutputs.Add(new OutputResult(outputResults));
                        }
                    }
                    numberOfOverallResults = Math.Max(submittedProgramOutputs.Count,compiledProgramOutputs.Count);
                }

                if (File.Exists(exePath) && File.Exists(compiledExePath))
                    possibleCheating = !CompareBothLists(); //If the 2 lists are different, there might be a possible cheating. Check manually.               
            }
        }


        //Check possible cheating when comparing submitted .exe results to compiled .exe results.
        public bool CompareBothLists()
        {
            if (submittedProgramOutputs.Count == compiledProgramOutputs.Count)
            {
                for (int i = 0; i < submittedProgramOutputs.Count; i++)
                    if (submittedProgramOutputs[i].GetResultOutput != compiledProgramOutputs[i].GetResultOutput)
                    {
                        return false; //Lists are different (possible cheating).
                    }
                return true; //Both lists are the same.
            }
            return false;
        }

        //Compares result to an output.
        public void CompareResultsToDesiredResults()
        {
            if (exeExists)
            {                
                int i = 0;
                foreach (SingleTestCase tc in TestCases.testCases)
                {
                    //Compare the desired result output in test case to actual result.
                    
                    //Handles the submitted program results.
                    if (submittedProgramOutputs.Count!=0)
                    {
                        if (tc.CompareOutput(submittedProgramOutputs[i].GetResultOutput)) //If the result matches the TC/TNC output.
                            submittedProgramOutputs[i].Match();
                        else
                            submittedProgramOutputs[i].Mismatch();
                    }
                    
                    //Handles the compiled program results.
                    if (compiledProgramOutputs.Count != 0)
                    {
                        if (tc.CompareOutput(compiledProgramOutputs[i].GetResultOutput)) //If the result matches the TC/TNC output.
                            compiledProgramOutputs[i].Match();
                        else
                            compiledProgramOutputs[i].Mismatch();
                    }
                    i++;
                }
            }
        }

        


        //Count the amount of matching results in the list.
        public int CorrectResultsCount()
        {

            int correctCountSubmitted = 0;
            int correctCountCompiled = 0;
            if (submittedProgramOutputs.Count > 0)
            {
                foreach (OutputResult result in submittedProgramOutputs)
                {
                    if (result.DidItMatch == true)
                        correctCountSubmitted++;
                }
            }

            if (compiledProgramOutputs.Count > 0)
            {
                foreach (OutputResult result in compiledProgramOutputs)
                {
                    if (result.DidItMatch == true)
                        correctCountCompiled++;
                }
            }

            return Math.Max(correctCountSubmitted, correctCountCompiled);
        }


        //Gets the percentage
        public string CorrectResultsPercentage()
        {
            decimal percent=0;            
            if(numberOfOverallResults != 0)
                percent = ((decimal)CorrectResultsCount())/ ((decimal)numberOfOverallResults)*100; //After all outputs go by testcases.
            double doublePercent =(double)percent;
            return percent.ToString()+"%";
        }

        //Get all of the (detailed) results from the current submission.
        public string GetAllSingleSubmissionResults()
        {
            string allResults = "No results.";
            if (submittedProgramOutputs.Count > 0)
            {
                allResults= "\r\n\r\n***********Submitted .exe results:\r\n";
                int i = 0;
                foreach (OutputResult r in submittedProgramOutputs)
                {
                    allResults += "****Input:\r\n\r\n" + TestCases.testCases[i].input + "\r\n\r\n";
                    allResults += "****Suppoesd output:\r\n\r\n"+TestCases.testCases[i].output + "\r\n\r\n";
                    allResults += "****Actual output:\r\n\r\n" + r.GetResultOutput + "\r\n\r\n";
                    if (r.DidItMatch)
                    {
                        allResults += "**Correct Output";
                        bool isTC = TestCases.testCases[i].equal;
                        if (!isTC)
                            allResults += " (is TNC)";
                        allResults +="\r\n\r\n";
                    }
                    else
                    {
                        allResults += "**Wrong Output!\r\n\r\n";
                        bool isTC = TestCases.testCases[i].equal;
                        if (!isTC)
                            allResults += " (is TNC)";
                        allResults += "\r\n\r\n";
                    }
                    i++;
                }
            }

            if (compiledProgramOutputs.Count > 0)
            {
                allResults += "\r\n\r\n***********Compiled .exe results:\r\n";

                int i = 0;
                foreach (OutputResult r in compiledProgramOutputs)
                {
                    allResults += "****Input:\r\n\r\n" + TestCases.testCases[i].input + "\r\n\r\n";
                    allResults += "****Supposed output:\r\n\r\n" + TestCases.testCases[i].output + "\r\n\r\n";
                    allResults += "****Actual output:\r\n\r\n" + r.GetResultOutput + "\r\n\r\n";
                    if (r.DidItMatch)
                    {
                        allResults += "**Correct Output";
                        bool isTC = TestCases.testCases[i].equal;
                        if (!isTC)
                            allResults += " (is TNC)";
                        allResults += "\r\n\r\n";
                    }
                    else
                    {
                        allResults += "**Wrong Output!\r\n\r\n";
                        bool isTC = TestCases.testCases[i].equal;
                        if (!isTC)
                            allResults += " (is TNC)";
                        allResults += "\r\n\r\n";
                    }
                    i++;
                }
            }
            return allResults;
        }

        public void CalculateFinalGrade(int codeWeight, int exeWeight, int correctResultsWeight)
        {
            this.finalGrade = (int)Grading(codeWeight, exeWeight, correctResultsWeight);
        }

        //This is a grading function that goes by weight for each part of the submission.
        public int Grading(int codeWeight, int exeWeight, int correctResultsWeight) 
        {
            decimal division=0;
            int grade = 0;

            if (codeExists)
                grade += codeWeight;
            if (exeExists)
                grade += exeWeight;
            if (numberOfOverallResults != 0)
            {
                division = (decimal)correctResultsWeight * (decimal)CorrectResultsCount() / (decimal)numberOfOverallResults;
            }
            grade += (int)division;

            return grade;
        }

    }
}
