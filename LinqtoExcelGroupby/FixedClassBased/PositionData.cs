using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqtoExcelGroupby.FixedClassBased
{
    class PositionData : GetExcelDataBase<PositionData>
    {
        private const string SHEET_NAME = "Position_Level";

        public string aIFSubAssetType { get; set; }
        public string exposure { get; set; }
        public string localMarketValue { get; set; }
        public string fXRateBase { get; set; }
        public string aIFStrategy { get; set; }
        public string businessDate { get; set; }
        public string derivativeCategory { get; set; }
        public string aIFMacroAssetType { get; set; }
        public string anySecuritiesBorrowingLending { get; set; }
        public string fundUniqueIdentifier { get; set; }

        public override void mappingTheColomn()
        {
            base.SetExcelData.AddMapping<PositionData>(k => k.aIFSubAssetType, "AIF_Sub_Asset_Type");
            base.SetExcelData.AddMapping<PositionData>(k => k.exposure, "Exposure");
            base.SetExcelData.AddMapping<PositionData>(k => k.localMarketValue, "Local_Market_Value");
            base.SetExcelData.AddMapping<PositionData>(k => k.fXRateBase, "FX_Rate_Base");
            base.SetExcelData.AddMapping<PositionData>(k => k.aIFStrategy, "AIF_Strategy");
            base.SetExcelData.AddMapping<PositionData>(k => k.businessDate, "Business_Date");
            base.SetExcelData.AddMapping<PositionData>(k => k.derivativeCategory, "Derivative_Category");
            base.SetExcelData.AddMapping<PositionData>(k => k.aIFMacroAssetType, "AIF_Macro_Asset_Type");
            base.SetExcelData.AddMapping<PositionData>(k => k.anySecuritiesBorrowingLending, "Any_Securities_Borrowing_Lending");
            base.SetExcelData.AddMapping<PositionData>(k => k.fundUniqueIdentifier, "Fund_Unique_Identifier");
        }

        public PositionData()
        {
            base.setSheetName = SHEET_NAME;
        }
    }
}
