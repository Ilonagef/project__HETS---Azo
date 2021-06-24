using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace HETS1Design
{
    /*A static class that can be accessed to compile the code and change options for compiler version/timeout.
     Do notice that TCC (the compiler we use, Tiny C Compiler, may not support all known c libraries and better
     not count on it for threading, unless you're willing to tweak the compiler so it will.
     We may make a possibility to choose another C compiler if we have time.*/
    public static class CodeChecker 
    {
        static string compilerPath64 = @"..\..\..\Assets\tcc\tcc.exe"; //We'll need to make sure this is the right directory.   
        static string compilerPath32 = @"..\..\..\Assets\tcc\i386-win32-tcc.exe";
        public static bool use32bitCompiler=false; //Whether submissions will be compiled in a 32 bit version of a compiler. Default is 64.
        public static int timeoutSeconds = 2; //Timeout period for .exe files (prevent infinite loops or deadlocks). Default is 5 seconds.

        public static string CompileCode(string codeFilePath)  //We'll need to pass a path into this function (Including file name).
        {
            string cFileName = Path.GetFileName(codeFilePath);
            string directoryName = Path.GetDirectoryName(codeFilePath);
            string compilerPath=compilerPath64;

            if (use32bitCompiler) //If tester chose the 32 bit version of the compiler.
            {
                compilerPath = compilerPath32;
            }

            ProcessStartInfo psi = new ProcessStartInfo(compilerPath, cFileName);
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.WorkingDirectory = directoryName; //Set process' working directory.

            Process p = new Process();
            p.StartInfo = psi; 
            p.Start();

            string compilerOutput = "No errors or warnings detected."; //If compiler doesn't have anything to complain about.
            using (StreamReader sr = p.StandardError)
            {
                if (sr.BaseStream.CanRead)
                {
                    compilerOutput = sr.ReadToEnd();
                }
            }

            if (compilerOutput == "")
            {
                compilerOutput= "No errors or warnings detected.";
            }

            p.Close();
            return compilerOutput;
        }

      
        public static string RunEXE(string exeFilePath, string input) //We'll need to get a path and a test case in here.
        {
            string exeFileName = Path.GetFileName(exeFilePath);
            string directoryName = Path.GetDirectoryName(exeFilePath);

            ProcessStartInfo psi = new ProcessStartInfo(exeFilePath);
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true; //false to see window
            psi.WorkingDirectory = directoryName; //Set process' working directory.
            
            Process p = new Process();
            p.StartInfo = psi;
            p.Start();


            string results ="";
            using (StreamWriter sw = p.StandardInput)
            {
                if (sw.BaseStream.CanWrite) //If it can't write then it's most likely a program with no input.
                {
                    sw.Write(input);
                }
            }

            /*In the following lines we have to start reading the program's output in a new task in order to 
             prevent a possible deadlock (infinite loop for example). If the reading isn't complete within the
             timeout period, the .exe file will be forced to close and the output will instead be "Timed out"!.*/

            var readErrors = Task.Run(() => results = p.StandardError.ReadToEnd()); 
            if (readErrors.Wait(TimeSpan.FromSeconds(timeoutSeconds)))
            {
                readErrors.Dispose();
            }
            else
            {
                p.Kill();
                p.Dispose();
                return "Timed out!" ;
            }

            if (results == "") //If there are no errors (runtime) go ahead and read output.
            {
                var readOutput = Task.Run(() => results = p.StandardOutput.ReadToEnd());
                if (readOutput.Wait(TimeSpan.FromSeconds(timeoutSeconds)))
                {
                    readOutput.Dispose();
                }
                else
                {
                    p.Kill();
                    p.Dispose();
                    return "Timed out!";               }
            }
            

            p.Close();
            p.Dispose();
            return results;
        }
    }
}
