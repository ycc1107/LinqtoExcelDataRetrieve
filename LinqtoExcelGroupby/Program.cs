using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using LinqtoExcelGroupby.DynamicClass;

namespace LinqtoExcelGroupby
{
    class Program
    {

        static void Main(string[] args)
        {
            const string pathToExcelFile = @"C:\Users\cyan\Desktop\AIFMD\AIFMD.xlsm";
            CreateExcelFile excelFile = new CreateExcelFile(pathToExcelFile);

            //BorrowingsData data1 = new BorrowingsData();
            //data1.SetExcelData = excelFile.getExcelFile;

            //var test296 =  data1.loadPositionData
            //                .Where(k => k.commitmentType != "C ommitted_and_Undrawn")
            //                .GroupBy(k => k.counterParty)
            //                .Select(k => new { counterParty = k.Key, totalAmount = k.Sum(x => Convert.ToDouble(x.amount_Base)) })
            //                .Take(5);

            //PositionData data2P = new PositionData();
            //data2P.SetExcelData = excelFile.getExcelFile;
           
            //var test294P = data2P.loadPositionData
            //                    .GroupBy(k => k.fundUniqueIdentifier)
            //                    .Select(k => new { id = k.Key, totalExposure = k.Sum(x => Math.Abs(Convert.ToDouble(x.exposure))) })
            //                    .ToList();

            //FundLevelData data2F = new FundLevelData();
            //data2F.SetExcelData = excelFile.getExcelFile;
            //var test294F = data2F.loadPositionData
            //                    .Select(k => new { netAssetValue = Convert.ToDouble(k.netAssetValue), factor = k.leverageGrossMethod, id = k.funduniqueIdentifier });

            //var dataJoined = (from positionLevel in test294P
            //                  join fundLevel in test294F on positionLevel.id equals fundLevel.id
            //                  select new { factor = fundLevel.factor, id = fundLevel.id, total = positionLevel.totalExposure, nav = fundLevel.netAssetValue })
            //                 .ToList();

            //foreach (var item in dataJoined)
            //{
            //    if (item.factor != null)
            //        Console.WriteLine(item.factor);
            //    else
            //        Console.WriteLine(item.nav/item.total);
            //}


            //foreach (var item in data1.loadPositionData)
            //{
            //    Console.WriteLine(item.amount_Base);
            //}

            List<FieldClass> classField = new List<FieldClass>();
            classField.Add(new FieldClass("AIF_Sub_Asset_Type",typeof(string)));

            DynamicDataObject testObject = new DynamicDataObject();
            testObject.fieldInformation = classField;
            testObject.CreateNewObject();

            testObject.SetExcelData = excelFile.getExcelFile;
            testObject.setSheetName = "Position_Level";
            var d = testObject.loadPositionData;

            Console.ReadKey();
        }
    }
}
