using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testing.Calculation;
using System.Diagnostics;
using Testing.UICheck;

namespace Testing
{
    public partial class Form1 : Form
    {
        private Boolean firstClick = true;

        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Name");
            listView1.Columns.Add("Value");
            label1.Text = "click go and wait";
            textBox1.Text = "AIFMD";
            
        }

        private void loadData()
        {
            string folderName = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            Console.WriteLine(folderName);
            string fileName = folderName + @"\data\\TestOutput.csv"; 
            var reader = new StreamReader(fileName);
            List<string> firstCol = new List<string>();
            List<string> secondCol = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine().Split(',');
                firstCol.Add(line[0]);
                if (line.Count() == 2)
                    secondCol.Add(line[1]);
                else
                    secondCol.Add(" ");
               
            }

            for(int i = 0 ; i < firstCol.Count(); i++)
            {
                ListViewItem row = new ListViewItem(firstCol[i]);
                row.SubItems.Add(secondCol[i]);
                listView1.Items.Add(row);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Stopwatch watch = new Stopwatch();
            watch.Start();
            string fileName = textBox1.Text;
            CheckInputFile fileCheck = new CheckInputFile(fileName);
            if (firstClick)
            {
                if (fileCheck.isFileNameWork == "")
                {
                    answerQuestions cacluation = new answerQuestions(fileName);
                    cacluation.run();
                    loadData();
                    watch.Stop();
                    TimeSpan ts = watch.Elapsed;
                    string elapsedTime = string.Format("{0:00}.{1:00}", ts.Minutes, ts.Seconds);
                    label1.Text = "RunTime : " + elapsedTime + " DONE";
                    firstClick = false;
                }
                else
                    label1.Text = fileCheck.isFileNameWork;
            }
            else
            {
                label1.Text = "Result already got.";
            }
        }

    }
}
