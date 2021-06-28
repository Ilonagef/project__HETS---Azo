using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Compression;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace HETS1Design.HETS_Classes
{
    class OpenFolder1
    {
        public static void ProcessDirectory(string dir, string path)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(dir);
            foreach (string file in fileEntries)
            {
                if (Submissions.submissions.Count == 0)  //Assuring we have at least 1 element in the list to use .Last()
                    Submissions.submissions.Add(new SingleSubmission(path));

                if (!(path.Contains(Submissions.submissions.Last().submitID)))
                    Submissions.submissions.Add(new SingleSubmission(path));

                if (file.Contains(".c") || file.Contains(".h"))
                {
                    string codePath = Path.Combine(path, Path.GetFileName(file));
                    File.Copy(file, codePath, true);
                    Submissions.submissions.Last().AddCode(codePath);

                }
                if (file.Contains(".exe"))
                {
                    if (!(Directory.Exists(path + @"\Exe\"))) //If it exists already, it may have more than 1 .exe file.
                        Directory.CreateDirectory(path + @"\Exe\");

                    string exePath = path + @"\Exe\" + Path.GetFileName(file);
                    File.Copy(file, exePath, true);
                    Submissions.submissions.Last().AddExe(exePath);
                }
            }

            // Recurse into subdirectories of this directory.
            string[] subdir = Directory.GetDirectories(dir);
            foreach (string sub in subdir)
            {
                string newPath = path + @"\" + new DirectoryInfo(dir).Name;
                if (!(Directory.Exists(newPath)))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (Submissions.submissions.Count == 0)  //Assuring we have at least 1 element in the list to use .Last()
                    Submissions.submissions.Add(new SingleSubmission(newPath));

                if (!(newPath.Contains(Submissions.submissions.Last().submitID)))
                    Submissions.submissions.Add(new SingleSubmission(newPath));

                ProcessDirectory(sub, newPath);
            }

        }
    }
}

