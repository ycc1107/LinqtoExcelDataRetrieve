using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;

namespace LinqtoExcelGroupby
{
    class GetFile
    {
        private ExcelQueryFactory _file;


        public GetFile(string path)
        {
            
        }

        public ExcelQueryFactory retrieveFile
        {
            get
            {
                return _file;
            }
        }
    }
}
