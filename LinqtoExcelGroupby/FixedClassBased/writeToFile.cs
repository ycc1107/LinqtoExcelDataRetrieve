using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LinqtoExcelGroupby.FixedClassBased
{
    class writeToFile
    {
        private string _path;
        public writeToFile(string path)
        {
            _path = path;
            if (File.Exists(_path))
            {
                Console.WriteLine("Clean the result");
                File.Delete(_path);
            }
        }

        public void writeFile(Dictionary<string,string> document,string questionTitle)
        {
            var csv = new StringBuilder();
            csv.Append(string.Format("{0}{1}", questionTitle, Environment.NewLine));

            foreach (KeyValuePair<string, string> item in document)
            {
                var newLine = string.Format("{0},{1}{2}",item.Key,item.Value,Environment.NewLine);
                csv.Append(newLine);
            }
            csv.Append(Environment.NewLine);
            try { File.AppendAllText(_path, csv.ToString()); }
            catch (IOException e) { Console.WriteLine("There is a exception during write file : {0}", e); }

            Console.WriteLine("Question {0} done", questionTitle);
               
        }
    }
}
