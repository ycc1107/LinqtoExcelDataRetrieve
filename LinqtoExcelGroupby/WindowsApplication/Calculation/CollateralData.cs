using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Calculation
{
    class CollateralData : GetExcelDataBase<CollateralData>
    {
        private const string SHEET_NAME = "Collateral";

        public string marketValueBase { get; set; }
        public string postedToExchangeOrCounterparty { get; set; }
        public string businessDate { get; set; }


        public override void mappingTheColomn()
        {
            base.SetExcelData.AddMapping<CollateralData>(k => k.marketValueBase, "Market_Value_Base");
            base.SetExcelData.AddMapping<CollateralData>(k => k.postedToExchangeOrCounterparty, "Posted_to_Exchange_or_Counterparty");
            base.SetExcelData.AddMapping<CollateralData>(k => k.businessDate, "Business_Date");

        }

        public CollateralData()
        {
            base.setSheetName = SHEET_NAME;
        }

    }
}
