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
        private const int FIELD_NAME_BEGIN_INDEX = 1;
        private const int FIELD_NAME_END_INDEX = 6;

        private string _sheetName;
        private List<T> _preloadedData;
        private List<string> _mappingList;

        public string setSheetName
        {
            set
            {
                _sheetName = value;
            }
        }

        public List<string> MappingList { get; set; }

        public ExcelQueryFactory ExcelData { set; get; }

        public virtual void mappingTheColomn()
        {
            if (MappingList.Count <= 0)
                throw new NotImplementedException("Do not have mapping list.");

            int fieldCounter = 0;
            Type classType = typeof(T);
            FieldInfo[] fieldInfo = classType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            if (MappingList.Count() - fieldInfo.Count() > 0)
                throw new NotImplementedException("more than field number");

            foreach (string mapping in MappingList)
                ExcelData.AddMapping(fieldInfo[fieldCounter++].Name.Substring(FIELD_NAME_BEGIN_INDEX, FIELD_NAME_END_INDEX), mapping);

        }

        public List<T> loadPositionData
        {
            get
            {

                if (_preloadedData == default(List<T>))
                {
                    mappingTheColomn();
                    _preloadedData = ExcelData.Worksheet<T>(_sheetName).ToList();
                }
                return _preloadedData;

            }
        }
    }
}
