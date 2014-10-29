using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using LinqtoExcelGroupby.MappingMethod;
using LinqtoExcelGroupby.FixedClassBased;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System.Linq.Dynamic;

namespace LinqtoExcelGroupby.InputAndInital
{
    class Inital
    {
        private const string POSISITION_LEVEL ="Position_Level";
        private const string BORROWINGS ="Borrowings";
        private const string FUND_LEVEL ="Fund_Level";
        private const string FUND_OWNERSHIP ="Fund_Ownership";
     
        private List<string> _mappingList = new List<string>();
        private Dictionary<string, List<LinqToExcel.Row>> _excelDataContain = new Dictionary<string, List<LinqToExcel.Row>>();
        private Dictionary<string, Dictionary<string, string>> _functionMethodValue = new Dictionary<string, Dictionary<string, string>>();
        private ExcelQueryFactory _excelFile;
        private MethodInfo[] _queryMethodList;


        private void getQueryMethod(string query)
        {
            List<string> tempFucntionMethodValue = query.Split('.').ToList();

            foreach (string item in tempFucntionMethodValue)
            {
                if (item.CompareTo("") == 0)
                    continue;
                List<string> temp = new List<string>(item.Split('|'));
                string cmd = temp[0].ToLower();
                Dictionary<string, string> colMethodValue = getQueryValue(temp.GetRange(1, temp.Count() - 1));
                _functionMethodValue.Add(cmd, colMethodValue);
            }

        }

        private Dictionary<string, string> getQueryValue(List<string> vale)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string valueItem in vale)
            {
                List<string> tempValues = new List<string>(valueItem.Split(','));
                foreach (string item in tempValues)
                {
                    string colMethod = "", colName;
                    int counter = 1;
                    List<string> temp = new List<string>(item.Split(' '));
                    if (temp[counter] == "")
                        colMethod = temp[counter--];
                    else
                    {
                        colMethod = temp[--counter];
                        counter++;
                    }
                    colName = temp[counter];

                    result.Add(colMethod, colName);
                }
            }
            return result;
        }

        private void setMappingToCol()
        {
            GetDataClass mappingData = new GetDataClass();
            List<string> colNames = new List<string>()
            {
                "AIF_Sub_Asset_Type",
                "Exposure",
                "Local_Market_Value"
            };
            mappingData.mappingFieldToColnum(colNames, _excelFile);

        }

        public Inital()
        {
            const string pathToExcelFile = @"C:\Users\cyan\Desktop\AIFMD\AIFMD.xlsm";
            CreateExcelFile excel = new CreateExcelFile(pathToExcelFile);
            _queryMethodList = typeof(System.Linq.Enumerable).GetMethods();
            _excelFile = excel.getExcelFile;
        }

        public Dictionary<string, List<LinqToExcel.Row>> GetData
        {
            get { return _excelDataContain; }
        }

        private List<LinqToExcel.Row> getData(string dataPosition)
        {
            List<LinqToExcel.Row> result = new List<LinqToExcel.Row>();

            if ( !_excelDataContain.TryGetValue(dataPosition, out result))
                _excelDataContain.Add(dataPosition, _excelFile.Worksheet (dataPosition).ToList());

            _excelDataContain.TryGetValue(dataPosition, out result);
            return result;

        }

        
        public void queryData(string query,List<string> dataPosition)
        {
            List<LinqToExcel.Row> temp = getData(POSISITION_LEVEL);

            getQueryMethod(query);

            foreach (string method in _functionMethodValue.Keys)    
            {
                Dictionary<string,string> value = new Dictionary<string,string>();
                foreach(MethodInfo queryMethod in _queryMethodList)
                {
                    if (queryMethod.Name.ToLower().CompareTo(method) == 0)
                    {
                        string name = "Exposure";
                        string groupedName = "AIF_Sub_Asset_Type";
                        int testValue = 10;
                        Expression<Func<LinqToExcel.Row, bool>> whereExpression =
                            (item) => Convert.ToDouble(item[name]) > testValue;

                        MethodInfo tempQueryMethod = typeof(LinqToExcel.Row).GetMethod(queryMethod.Name,new[]{typeof(System.Linq.Expressions.LabelExpression)});
                        var test = (from item in temp
                                    where Convert.ToDouble(item[name]) < 10000
                                    group item by item[groupedName] into A
                                    select  new { id = A.Key, sum = A.Sum(x=> Convert.ToDouble(x[name]))});
                        
                        var 
                                     

                        
                        

                        //ParameterInfo[] tempPara = queryMethod.GetParameters();
                        //object[] paramerterArray = new object[tempPara.Count()];
                        //int i = 0;
                        //foreach(ParameterInfo  para in tempPara)
                        //{
                        //    if(i == 0)
                        //        paramerterArray[i++] = from rowItem in temp select rowItem;
                        //    else
                        //        paramerterArray[i++] = "AIF_Sub_Asset_Type";
                        //}
                        //queryMethod.Invoke(temp, new object[] { paramerterArray });
                    }
                }
                value = _functionMethodValue[method];
            }

            //thisMethods.Invoke(this, null);

        }
        
        
        
        
    }
}
