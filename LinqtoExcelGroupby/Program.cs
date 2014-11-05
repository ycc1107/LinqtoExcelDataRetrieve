using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using LinqtoExcelGroupby.MappingMethod;
using LinqtoExcelGroupby.FixedClassBased;
using System.Linq.Expressions;
using LinqtoExcelGroupby.InputAndInital;
using System.Reflection;
using System.Linq.Expressions;

namespace LinqtoExcelGroupby
{
    class Program
    {

        static void Main(string[] args)
        {
            answerQuestions newAQ = new answerQuestions();

            newAQ.run();

            Console.WriteLine("doen");
        }
     }
}
