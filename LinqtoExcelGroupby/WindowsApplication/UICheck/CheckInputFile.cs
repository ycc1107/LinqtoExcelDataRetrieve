using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;

namespace Testing.UICheck
{
    class CheckInputFile
    {
        private string _resultString = "";
        public CheckInputFile(string fileNameInput)
        {
            string folderName = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            string fileName = folderName + @"\data\" + fileNameInput + @".xlsm";
            if (!File.Exists(fileName) || fileNameInput == "")
                _resultString = "File can not be found.";
        }
        public string isFileNameWork
        {
            get{return _resultString;}
        }
    }
}
