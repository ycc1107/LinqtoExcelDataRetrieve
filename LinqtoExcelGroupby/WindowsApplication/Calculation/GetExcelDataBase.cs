using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using System.Reflection;


namespace Testing.Calculation
{
    class GetExcelDataBase<T>
    {
        private string _sheetName;
        private ExcelQueryFactory _excelFile;
        private List<T> _preloadedData;
        private List<string> _mappingList;

        public string setSheetName
        {
            set
            {
                _sheetName = value;
            }
        }

        public List<string> setMappingList
        {
            set
            {
                _mappingList = value;
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

        public virtual void mappingTheColomn()
        {
            if (_mappingList.Count <= 0)
                throw new NotImplementedException("Do not have mapping list.");

            int fieldCounter = 0;
            Type classType = typeof(T);
            FieldInfo[] fieldInfo = classType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            int fieldDifferent = _mappingList.Count() - fieldInfo.Count();
            //createTempField(fieldDifferent < 0 ? 0 : fieldDifferent);

            foreach (string mapping in _mappingList)
                SetExcelData.AddMapping(fieldInfo[fieldCounter++].Name, mapping);
            //fieldCounter++;
        }

        public List<T> loadPositionData
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
