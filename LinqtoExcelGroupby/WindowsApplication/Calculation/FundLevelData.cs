using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Calculation
{

    class FundLevelData : GetExcelDataBase<FundLevelData>
    {
        private const string SHEET_NAME = "Fund_Level";

        public string funduniqueIdentifier { get; set; }
        public string netAssetValue { get; set; }
        public string leverageGrossMethod { get; set; }
        public string fxBaseRate { get; set; }
        public string netAssetValueBase { get; set; }

        public override void mappingTheColomn()
        {
            base.SetExcelData.AddMapping<FundLevelData>(k => k.funduniqueIdentifier, "Fund_Unique_Identifier");
            base.SetExcelData.AddMapping<FundLevelData>(k => k.netAssetValue, "Net_Asset_Value");
            base.SetExcelData.AddMapping<FundLevelData>(k => k.leverageGrossMethod, "Leverage_Gross_Method");
            base.SetExcelData.AddMapping<FundLevelData>(k => k.fxBaseRate, "Base_EUR_FX_Rate");
            base.SetExcelData.AddMapping<FundLevelData>(k => k.netAssetValueBase, "Net_Asset_Value_Base");
        }

        public FundLevelData()
        {
            base.setSheetName = SHEET_NAME;
        }
    }
}
