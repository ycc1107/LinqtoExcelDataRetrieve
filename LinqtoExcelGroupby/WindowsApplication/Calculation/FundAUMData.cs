using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Calculation
{
    class FundAUMData : GetExcelDataBase<FundAUMData>
    {
        private const string SHEET_NAME = "Fund_AUM";

        public string fundType { get; set; }
        public string fundUniqueIdentifier { get; set; }

        public override void mappingTheColomn()
        {
            base.SetExcelData.AddMapping<FundAUMData>(k => k.fundType, "Fund_Type");
            base.SetExcelData.AddMapping<FundAUMData>(k => k.fundUniqueIdentifier, "Fund_Unique_Identifier");
        }

        public FundAUMData()
        {
            base.setSheetName = SHEET_NAME;
        }
    }
}
