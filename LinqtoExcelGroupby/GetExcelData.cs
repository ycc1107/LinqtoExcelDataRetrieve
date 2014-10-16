using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;

namespace LinqtoExcelGroupby
{
    class GetExcelDataBase<T>
    {
        private string _sheetName;
        private ExcelQueryFactory _excelFile;
        private List<T> _preloadedData;

        public string setSheetName
        {
            set
            {
                _sheetName = value;
            }
        }

        public ExcelQueryFactory SetExcelData
        {
            set
            {
                _excelFile = value;
            }
            get
            {
                return _excelFile;
            }
        }

        public virtual void mappingTheColomn() { Console.WriteLine("no override"); }

        public  List<T> loadPositionData
        {  
            get
            {

                if (_preloadedData == default(List<T>))
                {
                    mappingTheColomn();
                    _preloadedData = _excelFile.Worksheet<T>(_sheetName).ToList();
                }
                return _preloadedData;

            }

        }
    }
}
