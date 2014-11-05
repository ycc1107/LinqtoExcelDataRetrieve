using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Testing.Calculation
{
    class BorrowingsData : GetExcelDataBase<BorrowingsData>
    {
        private const string SHEET_NAME = "Borrowings";

        public string amount_Base { get; set; }
        public string commitmentType { get; set; }
        public string counterParty { get; set; }

        public override void mappingTheColomn()
        {
            base.SetExcelData.AddMapping<BorrowingsData>(k => k.amount_Base, "Amount_Base");
            base.SetExcelData.AddMapping<BorrowingsData>(k => k.commitmentType, "Commitment_Type");
            base.SetExcelData.AddMapping<BorrowingsData>(k => k.counterParty, "CounterParty");
        }

        public BorrowingsData()
        {
            base.setSheetName = SHEET_NAME;
        }
    }
}