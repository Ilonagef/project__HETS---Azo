using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace HETS1Design
{
    public class SingleTestCase
    {
        public string input { private set; get; } //Input for test case.
        public string output { private set; get; } //Desired output for test case given input.
        public bool equal { private set; get; } //Whether the result output must be equal or NOT equal to desired output. (TC/TNC).
        public bool hasBoundInText { private set;  get; } 
        public bool hasEPInText { private set;  get; }


        //A single test case containts input, desired output and whether is TC ot TNC.
        public SingleTestCase(string input, string output, bool equal)
        {
            this.input = input;
            this.output = output;
            this.equal = equal;

            if (BoundaryScan(input)) //Check for special keywords.
                hasBoundInText = true;
            else
                hasBoundInText = false;

            if (EPScan(input))
                hasEPInText = true;
            else
                hasEPInText = false;
        }

        //Compares TC (desired) output to actual program output.
        public bool CompareOutput(string resultOutput)
        {
            if (this.output == resultOutput)
                return this.equal && true; //Returns true only if it's both equal AND it supposed to be equal.

            else return !this.equal; //Returns true when it's NOT supposed to be equal and false where it's supposed to be but isn't.            
        }

        //Scans if there are Boundary Values keywords
        private bool BoundaryScan(string input)
        {
            using (StringReader sr = new StringReader(input))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("__[Bound]"))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        //Scans if there are Equivalence Partitioning keywords
        private bool EPScan(string input) 
        {
            using (StringReader sr = new StringReader(input))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("__[EP]")) 
                    {
                        return true;
                    }
                }
                return false;
            }
        }


        //*****************************************************************************************
        /*The next functions may be fairly complicated. Thet consist of many small operations 
         in order to fultill what's needed of them. So, in order not to fill the editor with green
         text, we'll just describe what they do instead of how they do it.*/
        //*****************************************************************************************



        public List<SingleTestCase> ReturnBoundaryTestCases() 
        {
            //Takes one (the first to scan) boundary syntax and multiply the test case by 5 with the boundary input range. (5 __[TC])  
            //or (5 __[TNC]) is it was originally TNC.
            //Multiplies the rest of the text including other boundary syntax and returns a list of 5 test cases with same output
            //but input according to boundary range. (lower limit, 1 above lower limit, middle, one below upper limit, upper limit)


            List<string> inputs = new List<string>(5);
            string[] newInputs = new string[5];
            for (int i = 0; i < 5; i++)
            {
                inputs.Add(this.input);
            }
                       
            //Split the text to elements and look for the keyword in each of of them.
            int boundInitialIndex = this.input.IndexOf("__[Bound] ");
            string[] elements = this.input.Split(' ','\n');
            List<string> bounds = new List<string>();
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == "__[Bound]")
                {
                    bounds.Add(elements[i] + " " + elements[i + 1] + " " + elements[i + 2]);
                }
            }


            int boundIndex = bounds[0].IndexOf("__[Bound] ");
            string numerals = (boundIndex < 0) ? bounds[0] : bounds[0].Remove(boundIndex, "__[Bound] ".Length);
            if (Regex.IsMatch(numerals, @"\d\s\d"))
            {
                string[] singulars = numerals.Split(' ');
                List<int> inputsIntegers = new List<int>(singulars.Length);
                for (int i = 0; i < singulars.Length; i++)
                {
                    inputsIntegers.Add(int.Parse(singulars[i]));
                }
                if(inputsIntegers[0]> inputsIntegers[1])
                {
                    throw new Exception("Lower boundary needs to be on left side!");
                }
                else
                {
                    if(inputsIntegers[0] == inputsIntegers[1])
                    {
                        newInputs[0] = (inputsIntegers[0]).ToString();
                        inputs[0] = (boundInitialIndex < 0) ? inputs[0] : inputs[0].Remove(boundInitialIndex, "__[Bound] ".Length + numerals.Length);
                        inputs[0] = inputs[0].Insert(boundInitialIndex, newInputs[0] + "");
                        List<SingleTestCase> singleBoundCaseList = new List<SingleTestCase>();
                        singleBoundCaseList.Add(new SingleTestCase(inputs[0], this.output, this.equal));
                        return singleBoundCaseList;

                    }
                    else
                    {
                        newInputs[0] = inputsIntegers[0].ToString();
                        newInputs[1] = (inputsIntegers[0] + 1).ToString();
                        newInputs[2] = ((int)(inputsIntegers[0] + inputsIntegers[1]) / 2).ToString();
                        newInputs[3] = (inputsIntegers[1] - 1).ToString();
                        newInputs[4] = (inputsIntegers[1]).ToString();
                    }
                }


                for (int i = 0; i < inputs.Count; i++)
                {
                    inputs[i] = (boundInitialIndex < 0) ? inputs[i] : inputs[i].Remove(boundInitialIndex, "__[Bound] ".Length + numerals.Length);
                    inputs[i] = inputs[i].Insert(boundInitialIndex, newInputs[i]);
                }

            }
            else
            {
                throw new Exception("Boundary isn't written well!");
            }
                        

            List<SingleTestCase> boundTests = new List<SingleTestCase>();
            for(int i=0;i<5;i++)
            {
                boundTests.Add(new SingleTestCase(inputs[i], this.output, this.equal));
            }
            boundTests = boundTests.GroupBy(x => x.input).Select(y => y.FirstOrDefault()).ToList();
            return boundTests;
        }

        public List<SingleTestCase> ReturnEPTestCases()
        {
            //Takes one (the first to scan) boundary syntax and multiply the test case by 7 with the boundary input range. (2 __[TNC], 5 __[TC]) 
            //or if this current case was already TNC then (2 __[TC], 5 __[TNC]).
            //Multiplies the rest of the text including other boundary syntax and returns a list of 7 test cases with same output
            //but input according to boundary range. (TC: lower limit, 1 above lower limit, middle, one below upper limit, upper limit)
            //(TNC: 1 below lower limit, 1 above upper limit):

            //We can use ReturnBoundaryTestCases for this to create the middle 5 Test Cases and create 2 other cases.
            List<string> inputs = new List<string>(7);
            string[] newInputs = new string[7];
            for (int i = 0; i < 7; i++)
            {
                inputs.Add(input);
            }

            //Split the text to elements and look for the keyword in each of of them.
            int boundInitialIndex = input.IndexOf("__[EP] ");
            string[] elements = input.Split(' ','\n');
            List<string> bounds = new List<string>();
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == "__[EP]")
                {
                    bounds.Add(elements[i] + " " + elements[i + 1] + " " + elements[i + 2]);
                }
            }


            int boundIndex = bounds[0].IndexOf("__[EP] ");

            string numerals = (boundIndex < 0) ? bounds[0] : bounds[0].Remove(boundIndex, "__[EP] ".Length);
            if (Regex.IsMatch(numerals, @"\d\s\d"))
            {
                string[] singulars = numerals.Split(' ');
                List<int> inputsIntegers = new List<int>(singulars.Length);
                for (int i = 0; i < singulars.Length; i++)
                {
                    inputsIntegers.Add(int.Parse(singulars[i]));
                }
                if(inputsIntegers[0]>inputsIntegers[1])
                {
                    throw new Exception("Lower EP needs to be on left side!");
                }
                else
                {
                    if(inputsIntegers[0] == inputsIntegers[1])
                    {
                        newInputs[0] = (inputsIntegers[0] - 1).ToString();
                        inputs[0] = (boundInitialIndex < 0) ? inputs[0] : inputs[0].Remove(boundInitialIndex, "__[EP] ".Length + numerals.Length);
                        inputs[0] = inputs[0].Insert(boundInitialIndex, newInputs[0] + "");

                        newInputs[1] = (inputsIntegers[0]).ToString();
                        inputs[1] = (boundInitialIndex < 0) ? inputs[1] : inputs[1].Remove(boundInitialIndex, "__[EP] ".Length + numerals.Length);
                        inputs[1] = inputs[1].Insert(boundInitialIndex, newInputs[1] + "");

                        newInputs[2] = (inputsIntegers[0] + 1).ToString();
                        inputs[2] = (boundInitialIndex < 0) ? inputs[2] : inputs[2].Remove(boundInitialIndex, "__[EP] ".Length + numerals.Length);
                        inputs[2] = inputs[2].Insert(boundInitialIndex, newInputs[2] + "");

                        List<SingleTestCase> singleEPCaseList = new List<SingleTestCase>();
                        singleEPCaseList.Add(new SingleTestCase(inputs[0], this.output, this.equal));
                        singleEPCaseList.Add(new SingleTestCase(inputs[1], this.output, this.equal));
                        singleEPCaseList.Add(new SingleTestCase(inputs[2], this.output, this.equal));
                        return singleEPCaseList;
                    }
                    else
                    {
                        newInputs[0] = (inputsIntegers[0] - 1).ToString();
                        newInputs[1] = inputsIntegers[0].ToString();
                        newInputs[2] = (inputsIntegers[0] + 1).ToString();
                        newInputs[3] = ((int)(inputsIntegers[0] + inputsIntegers[1]) / 2).ToString();
                        newInputs[4] = (inputsIntegers[1] - 1).ToString();
                        newInputs[5] = (inputsIntegers[1]).ToString();
                        newInputs[6] = (inputsIntegers[1] + 1).ToString();
                    }
                }
                

                for (int i = 0; i < inputs.Count; i++)
                {
                    inputs[i] = (boundInitialIndex < 0) ? inputs[i] : inputs[i].Remove(boundInitialIndex, "__[EP] ".Length + numerals.Length);
                    inputs[i] = inputs[i].Insert(boundInitialIndex, newInputs[i] + "");
                }

            }
            else
            {
                throw new Exception("EP isn't written well!");
            }


            List<SingleTestCase> epTests = new List<SingleTestCase>();
            epTests.Add(new SingleTestCase(inputs[0], this.output, !this.equal));
            for (int i = 1; i <= 5; i++)
            {
                epTests.Add(new SingleTestCase(inputs[i], this.output, this.equal));
            }
            epTests.Add(new SingleTestCase(inputs[6], this.output, !this.equal));
            epTests = epTests.GroupBy(x => x.input).Select(y => y.FirstOrDefault()).ToList();
            return epTests;

        }


    }
}
