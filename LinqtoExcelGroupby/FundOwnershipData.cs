using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqtoExcelGroupby
{

    class FundOwnershipData : GetExcelDataBase<FundOwnershipData>
    {
        private const string SHEET_NAME = "Fund_Ownership";

        public string funduniqueIdentifier { get; set; }
        public string percentageOwnership { get; set; }
        public string businessDate { get; set; }
        public string netAssetValue { get; set; }

        public override void mappingTheColomn()
        {
            base.SetExcelData.AddMapping<FundOwnershipData>(k => k.funduniqueIdentifier, "Fund_Unique_Identifier");
            base.SetExcelData.AddMapping<FundOwnershipData>(k => k.percentageOwnership, "Percentage_Ownership");
            base.SetExcelData.AddMapping<FundOwnershipData>(k => k.businessDate, "Business_Date");
         
        }

        public FundOwnershipData()
        {
            base.setSheetName = SHEET_NAME;
        }
    }
}
