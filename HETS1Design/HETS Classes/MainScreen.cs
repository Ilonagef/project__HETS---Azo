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
using System.Diagnostics.CodeAnalysis;
using Microsoft.WindowsAPICodePack.Dialogs;
namespace HETS1Design
{
    /*We are excluding this from code coverage since we'll be testing these functions in
     MainScreenLogic class.*/
    [ExcludeFromCodeCoverage]
    public partial class MainScreen : Form
    {
        
        
        public MainScreen()
        {
            InitializeComponent();
        }


        private void MainScreen_Load(object sender, EventArgs e)
        {
            MainScreenLogic.OnMainScreenLoad(this.menuCodeWeight, this.menuExeWeight, this.menuResultsWeight);
        }

        private void MainScreen_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            MainScreenLogic.DisplayGuideHelpBox();
        }

        private void btnCompile_Click(object sender, EventArgs e)
        { 
            MainScreenLogic.CompileHelper(this.btnCompile, this.txtArchivePath, this.txtInputPath, this.txtOutputPath);
        }
        /*new*/
        //delegate void CompCallback();
        public void CmdCompile()
        {
          
          if(this.btnCompile.InvokeRequired )
           {
                Action comp = delegate { CmdCompile(); };
                this.Invoke(comp);
               // CompCallback d = new CompCallback(CmdCompile);
                //this.Invoke(d , new object[] { });
           }
          else
          {
                
                btnCompile_Click(null, null);
          }  
        }


        private void btnRunProgram_Click(object sender, EventArgs e)
        {
            MainScreenLogic.RunHelper(this.btnRunProgram, this.txtArchivePath, this.txtInputPath, this.txtOutputPath, this.btnResults);
        }

        /**/
        public void CmdRun()
        {
            if(this.btnRunProgram.InvokeRequired)
            {
                Action run = delegate { CmdRun(); };
                this.Invoke(run);
            }
            else
            {
                btnRunProgram_Click(null, null);
            }
         
        }
     

        private void btnResults_Click(object sender, EventArgs e)
        {
            
                btnResults.Enabled = false;
                MainScreenLogic.OnShowResults(this.dataGridResults, this.btnDetailedResults);


        }

        //בשביל בדיקות אוטומטיות
        //delegate void ResultsCallback();
        public void CmdResults()
        {
            if (this.btnResults.InvokeRequired)
            {
                Action resultes = delegate { CmdResults(); };
                this.Invoke(resultes);
                //ResultsCallback d = new ResultsCallback(CmdResults);
                //this.Invoke(d, new object[] { });
            }
            else
            {
                this.btnResults_Click(null, null);

            }
        }

       

        private void btnDetailedResults_Click(object sender, EventArgs e)
        {
            MainScreenLogic.OnSaveDetailedResults(this.txtArchivePath);
        }

        public void CmdResultsFile()
        {
            if(this.btnDetailedResults.InvokeRequired)
            {
                Action resFile = delegate { CmdResultsFile(); };
                this.Invoke(resFile);
            }
            else
            {
                btnDetailedResults_Click(null, null);
            }
            

        }

        private void btnSaveIO_Click(object sender, EventArgs e)
        {
            MainScreenLogic.OnButtonSaveIOClick(this.txtInputPath, this.txtOutputPath);
            
        }

        private void btnAddTestCase_Click(object sender, EventArgs e)
        {
            MainScreenLogic.OnButtonAddTestCaseClick(this.radioTC, this.radioTNC, this.txtInputAppend, this.txtOutputAppend);
        }


        private void btnBrowseArchive_Click(object sender, EventArgs e)
        {
            MainScreenLogic.PrepareFileDialog("ZIP Archive files (*.zip)|*.zip", openArchiveDialog);
        }

        //public void cmdBrowseArchive(string num1 , string num)

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            MainScreenLogic.PrepareFileDialog("Text files (*.txt)|*.txt", openInputDialog);
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            MainScreenLogic.PrepareFileDialog("Text files (*.txt)|*.txt", openOutputDialog);
        }

        private void openArchiveDialog_FileOk(object sender, CancelEventArgs e)
        {
            MainScreenLogic.OpenArchiveFile(this.openArchiveDialog, this.txtArchivePath, this.btnResults, this.btnDetailedResults);
        }

        /*בשביל בדיקות אוטומטיות
         This function like openArchiveDialog*/
        delegate void BrowseZIPCallback();
        public void CmdOpenArch()
        {
            if (this.txtArchivePath.InvokeRequired)
            {

                BrowseZIPCallback d = new BrowseZIPCallback(CmdOpenArch);
                this.Invoke(d, new object[] { });
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                 ofd.FileName = @"D:\שנה ג\סמסטר ב\אימות ובדיקת תוכנה\project2_1\Project Code\HETS - Azo\Assets\New Test_ilonaAbdala\ZipForTest.zip";
                 MainScreenLogic.OpenArchiveFile(ofd, this.txtArchivePath, this.btnResults, this.btnDetailedResults);
            }
            

            
        }
       
 
        private void openInputDialog_FileOk(object sender, CancelEventArgs e)
        {
            MainScreenLogic.OpenInputFile(this.openInputDialog, this.txtInputPath, this.txtOutputPath, this.btnAddTestCase, this.btnSaveIO);
        }
        /**/
        public void CmdOpenInput(OpenFileDialog input)
        {
           // string pathInput = input.FileName;
            if(this.txtInputPath.InvokeRequired == true)
            {
                this.Invoke(new Action<OpenFileDialog>(CmdOpenInput) , new object[] {input });
                
            }
            else
            {
                MainScreenLogic.OpenInputFile(input, this.txtInputPath, this.txtOutputPath, this.btnAddTestCase, this.btnSaveIO);
            }
            
            //openInputDialog_FileOk(null, null);
        }

        private void openOutputDialog_FileOk(object sender, CancelEventArgs e)
        {
            MainScreenLogic.OpenOutputFile(this.openOutputDialog, this.txtOutputPath, this.txtInputPath, this.btnAddTestCase, this.btnSaveIO);
        }
        /**/
        public void CmdOpenOutput(OpenFileDialog output)
        {
            if (this.txtOutputPath.InvokeRequired == true)
            {
                this.Invoke(new Action<OpenFileDialog>(CmdOpenOutput), new object[] { output });
            }
            else
            {
                MainScreenLogic.OpenOutputFile(output, this.txtOutputPath, this.txtInputPath, this.btnAddTestCase, this.btnSaveIO);
            }
           
            //openOutputDialog_FileOk(null, null);

        }
        private void menuCodeWeight_ValueChanged(object sender, EventArgs e)
        {
            MainScreenLogic.LimitWeightsChange(this.menuCodeWeight, this.menuExeWeight, this.menuResultsWeight);
        }

        private void menuExeGrade_ValueChanged(object sender, EventArgs e)
        {
            MainScreenLogic.LimitWeightsChange(this.menuCodeWeight, this.menuExeWeight, this.menuResultsWeight);
        }

        private void menuResultsGrade_ValueChanged(object sender, EventArgs e)
        {
            MainScreenLogic.LimitWeightsChange(this.menuCodeWeight, this.menuExeWeight, this.menuResultsWeight);
        }

        private void checkBoxEnableGrading_CheckedChanged(object sender, EventArgs e)
        {
            MainScreenLogic.EnableGradingCheckedChange(this.checkBoxEnableGrading, this.menuCodeWeight, this.menuExeWeight, this.menuResultsWeight);
        }

        private void radioButton64BitCompiler_CheckedChanged(object sender, EventArgs e)
        {
            MainScreenLogic.Option64BitCompilerChange();
        }

        private void radioButton32BitCompiler_CheckedChanged(object sender, EventArgs e)
        {
            MainScreenLogic.Option32BitCompilerChange();
        }


        private void timeoutNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            MainScreenLogic.TimeoutValueChange(timeoutNumUpDown);
        }

        private void radioBtnExecutable_CheckedChanged(object sender, EventArgs e)
        {
            MainScreenLogic.OnCheckCodeRadioChange(this.btnCompile);
        }
        public void CmdRadioCode()
        {
            if(this.radioBtnExecutable.InvokeRequired)
            {
                Action radioCode = delegate { CmdRadioCode(); };
                this.Invoke(radioCode);
            }
            else
            {
                Submissions.checkCode = true;
                Submissions.checkExe = false;
                btnCompile.Enabled = true;
            }
        }

        private void radioBtnCode_CheckedChanged(object sender, EventArgs e)
        {
            MainScreenLogic.OnCheckExeRadioChange(this.btnCompile);
        }

        public void CmdRadioExec()
        {
            if(this.radioBtnCode.InvokeRequired)
            {
                Action radioExec = delegate { CmdRadioExec(); };
                this.Invoke(radioExec);
            }
            else
            {
                Submissions.checkCode = false;
                Submissions.checkExe = true;
                btnCompile.Enabled = false;
            }
        }

        private void radioBtnBothExeAndCode_CheckedChanged(object sender, EventArgs e)
        {
            MainScreenLogic.OnCheckBothRadioChange(this.btnCompile);
        }

        public void CmdRadioBoth()
        {
            if(this.radioBtnBothExeAndCode.InvokeRequired)
            {
                Action radioBoth = delegate { CmdRadioBoth(); };
                this.Invoke(radioBoth);
            }
            else
            {
                Submissions.checkCode = true;
                Submissions.checkExe = true;
                btnCompile.Enabled = true;
            }
        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            MainScreenLogic.OnExportToCSV(saveCSVFile, dataGridResults);
        }

        private void btnBrowserFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.ShowDialog();
            MainScreenLogic.OpenFolder(dialog, this.txtArchivePath, this.btnResults, this.btnDetailedResults);
        }
        /**/
        delegate void BrowseFolderCallback();
        public void CmdOpenFolder()
        {
            
            if (this.txtArchivePath.InvokeRequired)
            {
                BrowseFolderCallback d = new BrowseFolderCallback(CmdOpenFolder);
                this.Invoke(d, new object[] { });
            }
            else
            {
                
                string path = @"D:\שנה ג\סמסטר ב\אימות ובדיקת תוכנה\project2_1\Project Code\HETS - Azo\Assets\New Test_ilonaAbdala\OpenFolderTest";
                MainScreenLogic.CmdOpenFolder(path, this.txtArchivePath, this.btnResults, this.btnDetailedResults);
            }


            

        }

      public void Cmd32Bits()
      {
            if(this.radioButton32BitCompiler.InvokeRequired)
            {
                Action bits32 = delegate { Cmd32Bits(); };
                this.Invoke(bits32);
            }
            else
            {
                this.radioButton32BitCompiler.Checked = true;
            }
      }

        public void CmdClose()
        {
            if(this.InvokeRequired)
            {
                Action close = delegate { CmdClose(); };
                this.Invoke(close);
            }
            else
            {
                this.Close();
            }


           
        }
      
    }
}
