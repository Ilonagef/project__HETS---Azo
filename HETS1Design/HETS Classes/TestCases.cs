using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HETS1Design
{
    public static class TestCases
    {

        public static List<SingleTestCase> testCases = new List<SingleTestCase>();  //The list of test cases. 
        //We save these incase tester wants to save added TCs into a new file.
        public static string inputText { get; private set; } 
        public static string outputText { get; private set; }

        /*Since this is a static function and we don't have a construct, we need to have a function that triggers
        once we have both Input and Output test cases. Activate this from MainScreen when both i/o filed loaded.*/
        public static void ExtractTestCasesFromText(string inputFilePath, string outputFilePath)
        {
            inputText = File.ReadAllText(inputFilePath);
            outputText = File.ReadAllText(outputFilePath);

            if (File.Exists(inputFilePath) && File.Exists(outputFilePath))
                TestCasesBuilder(inputText, outputText);
            else
                MessageBox.Show("Files are missing!");
        }

        //Add a new Test Case (one at a time, without TC/TNC keywords from text boxes and activate MultiplyTestCasesBy functions.
        public static void OnAddTestCase(string inputBox, string outputBox, bool isTC)
        {
            if (inputBox != "" && outputBox != "")
            {
                if (isTC)
                {
                    inputText += "\r\n__[TC]" + "\r\n" + inputBox;
                    outputText += "\r\n__[TC]" + "\r\n" + outputBox;
                }

                else
                {
                    inputText += "\r\n__[TNC]" + "\r\n" + inputBox;
                    outputText += "\r\n__[TNC]" + "\r\n" + inputBox;
                }

                testCases.Add(new SingleTestCase(inputBox, outputBox, isTC));

                testCases = MultiplyTestCasesByBoundary(testCases.ToList());
                testCases = MultiplyTestCasesByEP(testCases.ToList());
               
            }
        }
        

        //Counts the amount of __[TC] and __[TNC] in the text. Will be used to gurantee symmetry.
        public static int CountTestCases(string fileToCheckContent) 
        {

            using (StringReader sr = new StringReader(fileToCheckContent))
            {
                int count = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("__[T"))
                    {
                        count++;
                    }                    
                }
                return count;
            }            
        }

        /*The following function will separate each test cases by __[TC] or __[TNC]
         and adds them to a string list element. The element will include the special keyword.
         Our keywords for test cases are __[TC] for Test Case and __[TNC] for Test NOT Case (Output must be different)
         This will have an impact only according to the input file, output may use any of them to separate.
         For example, for an input file that contains:
         __[TC]
         9 5             
         __[TNC]
         3 4
         1

        will create the list 
        <"__[TC]9 5\n", "__[TNC]3 4\n1\n">       
         */
        
            
        //Description above.
        public static List<string> TestCasesSeparator(string textFileContent)
        {
            List<string> testCasesList = new List<string>();

            using (StringReader sr = new StringReader(textFileContent))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("__[T"))
                    {
                        testCasesList.Add(line);
                    }
                    else
                    testCasesList[testCasesList.Count - 1] +="\r\n"+ line;
                }
            }                     
            return testCasesList;
        }

        //Checks whether a test case is TC or TNC before removing the keyword.
        public static bool TC_or_TNC(string testCase)
        {
            if (testCase.Contains("__[TC]")) //First line must contain either TC or TNC.
                return true;
            return false; //It can only be TNC if not TC because otherwise it wouldn't be the first line.
        }

        //Removes the special keyword after separating to test cases.
        public static string RemoveTCTNC(string testCase) 
        {
            var lines = Regex.Split(testCase, "\r\n|\r|\n").Skip(1); //TODO: Use Environment.NewLine. if doesn't work.
            string testCaseWithoutKeyword = string.Join(Environment.NewLine, lines.ToArray());

            if (testCaseWithoutKeyword != null) //If tester forgot to write anything below keyword.
                return testCaseWithoutKeyword;
            else
                return ""; //Or for cases with no input.
        }

        //Fills the test cases list according the the Input/Output files.
        public static void TestCasesBuilder(string inputFileText, string outputFileText)
        {
            bool isTC;
            bool ISTNC;
            int countInput, countOutput = 0;
            countInput = CountTestCases(inputFileText);
            countOutput = CountTestCases(outputFileText);
            
            //Check if the input/output files content format is correct before building.
            if(countInput == countOutput)//when the total count  is same in input and output files
            {
                if(countInput == 0 && countOutput==0)//check if the bouth files are empty(no TC and TNC)
                {
                    throw new Exception("Test cases number in input and output file is empty !");
                }
                else
                {
                    List<String> input = TestCasesSeparator(inputFileText);
                    List<String> output = TestCasesSeparator(outputFileText);

                    for (int i = 0; i < input.Count(); i++)
                    {
                        isTC = TC_or_TNC(input[i]);
                        ISTNC = TC_or_TNC(output[i]);
                        if (i == 0)
                        {
                            if(isTC == false || ISTNC == false)/*one/bouth files dont startt with TC*/
                            {
                                throw new Exception("The test file Must Start with __[TC]");
                            }
                          
                        }
                        if (isTC != ISTNC)
                        {
                            throw new Exception("TC and TNC in input and output file must be maching!.");
                        }
                        isTC = TC_or_TNC(input[i]);
                        input[i] = RemoveTCTNC(input[i]);
                        output[i] = RemoveTCTNC(output[i]);
                        testCases.Add(new SingleTestCase(input[i], output[i], isTC));
                    }

                    testCases = MultiplyTestCasesByBoundary(testCases.ToList());
                    testCases = MultiplyTestCasesByEP(testCases.ToList());
                }
            }
            else//when the total count is not same in the bouth of files
            {
                if(countInput==0 && countOutput != 0)
                {
                    
                        throw new Exception("The number of TC  and TNC in input file is zero.");
                    
                  
                }
                else if (countInput != 0 && countOutput == 0)
                {
                   
                        throw new Exception("The number of TC and TNC in outPut file is zero.");
                    
                }
                else if (countInput != countOutput)
                {
                    throw new Exception("must be withe the same total count");
                }
            }
            
        }

        /**************************************************************************************************
        The following functions may have a shallow/deep copy problem, therefore, when calling it on the 
        construct call it like this: 
        testCases = MultiplyTestCasesByBoundary(testCases.ToList());
        ***************************************************************************************************/

        //Recursive function to get rid of __[Bound] keyword and add the appropriate 5 test cases
        public static List<SingleTestCase> MultiplyTestCasesByBoundary(List<SingleTestCase> testCasesCopy) 
        {
            int i = 0;
            foreach (SingleTestCase tc in testCasesCopy.ToList()) //testCases.ToList() is a new temporary copy of testCases.
            {
                if (tc.hasBoundInText) //If current tc (of the temp test cases list has the keyword)
                {
                    testCasesCopy.AddRange(tc.ReturnBoundaryTestCases()); //Add 7 test cases to the original passed testCasesCopy list.
                    testCasesCopy.RemoveAt(i); //Removing from the passed list will need to have index i stay in place (they all move back).
                }
                else
                i++; //Advance index at the original list only if nothing was removed.
            }

            bool tcListisClearofBound = true;
            foreach (SingleTestCase tc in testCasesCopy) //Checking the original list for more keywords.
            {
                if (tc.hasBoundInText)
                {
                    tcListisClearofBound = false;
                }
            }

            if (tcListisClearofBound == true)
                return testCasesCopy;
            else
                return MultiplyTestCasesByBoundary(testCasesCopy);
        }

        //Recursive function to get rid of __[EP] keyword and add the appropriate 7 test cases.
        public static List<SingleTestCase> MultiplyTestCasesByEP(List<SingleTestCase> testCasesCopy)
        {
            int i = 0;
            foreach (SingleTestCase tc in testCasesCopy.ToList()) //testCases.ToList() is a new temporary copy of testCases.
            {
                if (tc.hasEPInText) //If current tc (of the temp test cases list has the keyword)
                {
                    testCasesCopy.AddRange(tc.ReturnEPTestCases()); //Add 7 test cases to the original passed testCasesCopy list.
                    testCasesCopy.RemoveAt(i); //Removing from the passed list will need to have index i stay in place (they all move back). 
                }
                else
                    i++; //Advance index at the original list only if nothing was removed.
            }

            bool tcListisClearofEP = true; 
            foreach (SingleTestCase tc in testCasesCopy) //Checking the original list for more keywords.
            {
                if (tc.hasEPInText)
                {
                    tcListisClearofEP = false;
                }
            }

            if (tcListisClearofEP == true)
                return testCasesCopy; //Return the list clear of keywords.
            else
                return MultiplyTestCasesByEP(testCasesCopy); //Recursive call.
        }


        public static void ResetTestCases()
        {
            testCases.Clear();
        }






    }
}
