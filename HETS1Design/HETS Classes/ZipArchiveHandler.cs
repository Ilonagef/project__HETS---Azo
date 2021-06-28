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


namespace HETS1Design
{
    public static class ZipArchiveHandler
    {
        //This funtion extracts .c, .h and .exe files from an archive file and saves their paths in a SingleSubmission. 
        //zipFile is the zip path.  isMasterZipDirectory indicates whether it's the master or inner (Zip in a zip) zip directory.
        public static void GetSubmissionData(string zipFile, bool isMasterZipDirectory)
        {

            if(Path.GetFullPath(zipFile).Contains(".zip"))
            {
                //Open ZIP Archive with Hebrew encoding.
                ZipArchive zip = new ZipArchive(File.OpenRead(zipFile), ZipArchiveMode.Read, false, Encoding.GetEncoding("cp862"));


                string extractToFolderName; //The name of the folder that will hold our extracted files.

                //Names the folder to extract into. Master zip is Codes To Check and inner zip keeps its original name. 
                if (isMasterZipDirectory)
                    extractToFolderName = @"\Codes To Check";
                else
                    extractToFolderName = @"\" + Path.GetFileName(zipFile).Substring(0, Path.GetFileName(zipFile).Length - 4); //Removes ".zip" from the name.


                string extractToFolder = Path.GetDirectoryName(zipFile) + extractToFolderName; //Full path of the folder/directory.


                //Makes sure there's no conflict with an existing folder.
                if (Directory.Exists(extractToFolder))
                {
                    Directory.Delete(extractToFolder, true);
                }

                Directory.CreateDirectory(extractToFolder); //Creates the folder, this needs the full path.

                //We need to make sure the entries are ordered by directories.
                var orderedZipEntries =
                    from entry in zip.Entries
                    orderby entry.FullName
                    select entry;


                foreach (ZipArchiveEntry zipEntry in orderedZipEntries) //This extracts the ZIP entries into directories in CodesToCheck Folder.
                {
                    string newDirectory = extractToFolder + @"\" + Path.GetDirectoryName(zipEntry.FullName); //To avoid unassigned error.


                    if (!(Directory.Exists(newDirectory))) //If that directory doesn't exist yet.
                    {
                        newDirectory = extractToFolder + @"\" + Path.GetDirectoryName(zipEntry.FullName);
                        Directory.CreateDirectory(newDirectory);    //Create a directory for a submission. Its name will serve as ID.

                        /*If it's a new directory that doesn't represent a new submission but still needs to be created
                        like an folder-in-folder situation, we make sure we don't make it into a new submission
                        so we check whether the new "submission" path doesn't happen to be a folder under the actual submission.*/

                        if (Submissions.submissions.Count == 0)  //Assuring we have at least 1 element in the list to use .Last()
                            Submissions.submissions.Add(new SingleSubmission(newDirectory));

                        if (!(newDirectory.Contains(Submissions.submissions.Last().submitID)))
                            Submissions.submissions.Add(new SingleSubmission(newDirectory));
                    }

                    string _2CharExtention = zipEntry.FullName.Substring(Math.Max(0, zipEntry.FullName.Length - 2)); //File extension.


                    if (_2CharExtention == ".c" || _2CharExtention == ".h") //If extension is .c (c code) or .h (c header).
                    {
                        string codePath = newDirectory + @"\" + Path.GetFileName(zipEntry.FullName);
                        zipEntry.ExtractToFile(codePath);

                        Submissions.submissions.Last().AddCode(codePath); //Always edit the newest Submission entry.
                    }



                    string _4CharExtension = zipEntry.FullName.Substring(Math.Max(0, zipEntry.FullName.Length - 4)); //File extension.


                    if (_4CharExtension == ".exe") //If extension is .exe put it in a new folder to prevent conflict with any TCC compiled .exe.
                    {
                        if (!(Directory.Exists(newDirectory + @"\Exe\"))) //If it exists already, it may have more than 1 .exe file.
                            Directory.CreateDirectory(newDirectory + @"\Exe\");

                        string exePath = newDirectory + @"\Exe\" + Path.GetFileName(zipEntry.FullName);
                        zipEntry.ExtractToFile(exePath);

                        Submissions.submissions.Last().AddExe(exePath);  //Always edit the newest Submission entry.
                    }


                    if (_4CharExtension == ".zip") //If extension is .zip, put it in a new folder with the same zip archive file name.
                    {
                        string innerZipPath = newDirectory + @"\" + Path.GetFileName(zipEntry.FullName);
                        zipEntry.ExtractToFile(innerZipPath);
                        GetSubmissionData(innerZipPath, false); //Recursive call. This is an inner zip, we pass false here.
                        File.Delete(innerZipPath);
                    }

                }

                zip.Dispose(); //Dispose once our data is in place.
            }

            else
            {
                string dirPath = Path.GetFullPath(zipFile);
                string folderName = "Code to Check";
                //string toFolder = Path.GetDirectoryName(zipFile) + @"\" + folderName;
                string toFolder = Path.GetDirectoryName(zipFile) + @"\" + Path.GetFileName(zipFile) + @"\" + folderName;//code to check
                //string toFolder = 
                //Makes sure there's no conflict with an existing folder.
                if (Directory.Exists(toFolder))
                {
                    Directory.Delete(toFolder, true);
                }

                Directory.CreateDirectory(toFolder); //Creates the folder, this needs the full path.
                ProcessDirectory(dirPath, toFolder);
                
            }
            

        }

        /*When its not Zip -> open AND add folsers and files*/
        public static void ProcessDirectory(string dir, string path)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(dir);
            foreach (string file in fileEntries)
            {
                

                if (Submissions.submissions.Count == 0)  //Assuring we have at least 1 element in the list to use .Last()
                    Submissions.submissions.Add(new SingleSubmission(dir));

                if (!(dir.Contains(Submissions.submissions.Last().submitID)))
                    Submissions.submissions.Add(new SingleSubmission(dir));

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
                if(!Path.GetFileName(sub).Contains("Code to Check"))
                {
                    string newPath = path + @"\" + new DirectoryInfo(sub).Name;
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
}
