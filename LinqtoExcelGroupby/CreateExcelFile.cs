﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;

namespace LinqtoExcelGroupby
{
    class CreateExcelFile
    {
        private ExcelQueryFactory _file;
    
        //private 

        public ExcelQueryFactory getExcelFile { get { return _file; } }

        public CreateExcelFile(string path)
        {
            _file = new ExcelQueryFactory(path);
        }
    }
}
