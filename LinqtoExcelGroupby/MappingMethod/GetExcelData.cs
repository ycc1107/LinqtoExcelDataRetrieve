using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using System.Reflection;


namespace LinqtoExcelGroupby.MappingMethod
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

        public List<string> setMappingList { get; set; }

        public ExcelQueryFactory SetExcelData { get; set; }

        public virtual void mappingTheColomn() 
        {
            if (setMappingList.Count <= 0)
                throw new NotImplementedException("Do not have mapping list.");

            int fieldCounter = 0;
            Type classType = typeof(T);
            FieldInfo[] fieldInfo = classType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            int fieldDifferent = setMappingList.Count() - fieldInfo.Count();

            foreach (string mapping in setMappingList)
                SetExcelData.AddMapping(fieldInfo[fieldCounter++].Name, mapping);
        }

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
