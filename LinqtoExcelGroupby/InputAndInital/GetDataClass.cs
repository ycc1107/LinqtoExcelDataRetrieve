using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;


namespace LinqtoExcelGroupby.InputAndInital
{
    class GetDataClass
    {
        public string field1 { get; set; }
        public string field2 { get; set; }
        public string field3 { get; set; }
        public string field4 { get; set; }
        public string field5 { get; set; }


        public void mappingFieldToColnum(List<string> colNames, ExcelQueryFactory excelFile)
        {
            foreach (string colName in colNames)
                excelFile.AddMapping<GetDataClass>(k => k.field1, colName);
        }
    }
}
