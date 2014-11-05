using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqtoExcelGroupby.FixedClassBased
{
    class answerQuestions
    {
        private string _readPath;
        private string _writePath;
        private BorrowingsData _borrowingData = new BorrowingsData();
        private PositionData _positionData = new PositionData();
        private FundLevelData _fundLevelData = new FundLevelData();
        private CollateralData _collateraldata = new CollateralData();
        private FundOwnershipData _fundOwnershipData = new FundOwnershipData();
        private FundAUMData _fundAumData = new FundAUMData();
        private writeToFile _writeBldr;
        private Dictionary<string, string> _answer;
       
        public answerQuestions()
        {
            _readPath = @"C:\Users\cyan\Desktop\AIFMD\AIFMD.xlsm";
            _writePath = @"C:\Users\cyan\Desktop\AIFMD\TestOutput.csv";
            CreateExcelFile excelFile = new CreateExcelFile(_readPath);
            _writeBldr = new writeToFile(_writePath);
            _answer = new Dictionary<string, string>();

            _borrowingData.SetExcelData = excelFile.getExcelFile;
            _positionData.SetExcelData = excelFile.getExcelFile;
            _fundLevelData.SetExcelData = excelFile.getExcelFile;
            _collateraldata.SetExcelData = excelFile.getExcelFile;
            _fundOwnershipData.SetExcelData = excelFile.getExcelFile;
            _fundAumData.SetExcelData = excelFile.getExcelFile;
        }

        public void run()
        {
            question130();
            question2287();
            question4296();
            question2294();
            question2208();
            question158();
        }

        private void question130()
        {
            _answer = new Dictionary<string, string>();

            var queriedValue = _positionData.loadPositionData
                            .GroupBy(k => k.aIFSubAssetType)
                            .Select(k => new { aIFSubAssetType = k.Key, sumExposure = k.Sum(x => Convert.ToDouble(x.exposure)) })
                            .OrderByDescending(k => k.sumExposure)
                            .Take(5);

            _answer.Add("AIF Macro Asset Type", "Sum Exposure");
            foreach (var element in queriedValue)
            {
                string temp = (element.aIFSubAssetType == null ? "NULL" : element.aIFSubAssetType).ToString();
                _answer.Add(temp, Convert.ToString(element.sumExposure));
            }
            _writeBldr.writeFile(_answer, "AIFM 1.30 - 32");
        }

        private void question2287()
        {
            double OTCExposure = 0;
            double ExchangeTradedExposure = 0;
            double MarginExchange = 0;
            double MarginCounterparty = 0;
            _answer = new Dictionary<string, string>();

            var queriedValue = _positionData.loadPositionData
                             .Select(k => new { Derivative_Category = string.IsNullOrEmpty(k.derivativeCategory) ? "NULL" : k.derivativeCategory.ToUpper(), Exposure = string.IsNullOrEmpty(k.exposure) ? 0.0 : double.Parse(k.exposure) })
                             .GroupBy(k => k.Derivative_Category)
                             .Select(k => new { derivativeCategory = k.Key, sumExposure = k.Sum(x => x.Exposure) });
            foreach (var element in queriedValue)
            {
                if (element.derivativeCategory.ToUpper() == "EXCHANGE-TRADED")
                    ExchangeTradedExposure = double.Parse(element.sumExposure.ToString());
                if (element.derivativeCategory.ToUpper() == "OTC")
                    OTCExposure = double.Parse(element.sumExposure.ToString());
            }

            

            var marginbytype = _collateraldata.loadPositionData
                                .Select(k => new { Posted_to_Exchange_or_Counterparty = string.IsNullOrEmpty(k.postedToExchangeOrCounterparty) ? "NULL" : k.postedToExchangeOrCounterparty.ToUpper(), Market_Value_Base = string.IsNullOrEmpty(k.marketValueBase) ? 0.0 : Convert.ToDouble(k.marketValueBase) })
                                .GroupBy(k => k.Posted_to_Exchange_or_Counterparty)
                                .Select(k => new { Posted_to_Exchange_or_Counterparty = k.Key, sumMargin = k.Sum(x => x.Market_Value_Base) });
            foreach (var element in marginbytype)
            {
                if (element.Posted_to_Exchange_or_Counterparty.ToUpper() == "EXCHANGE")
                    MarginExchange = double.Parse(element.sumMargin.ToString());
                if (element.Posted_to_Exchange_or_Counterparty.ToUpper() == "COUNTERPARTY")
                    MarginCounterparty = double.Parse(element.sumMargin.ToString());
            }

            
            _answer.Add("287",Convert.ToString(ExchangeTradedExposure - MarginExchange));
            _answer.Add("288", Convert.ToString(OTCExposure - MarginCounterparty));
            _writeBldr.writeFile(_answer, "AIFM 2.287 - 289");
        }

        private void question4296()
        {
            _answer = new Dictionary<string, string>();

            var queriedValue = _borrowingData.loadPositionData
                            .Where(k => k.commitmentType != "Committed_and_Undrawn")
                            .GroupBy(k => k.counterParty)
                            .Select(k => new { counterParty = k.Key, totalAmount = k.Sum(x => Convert.ToDouble(x.amount_Base)) })
                            .OrderByDescending( k=>k.totalAmount)
                            .Take(5);

            _answer.Add("Counter Party", "Total Amount");
            foreach (var element in queriedValue)
            {
                _answer.Add(element.counterParty, Convert.ToString(element.totalAmount));
            }
            _writeBldr.writeFile(_answer, "AIFM 4.296 - 301");
        }

        private void question2294()
        {
            _answer = new Dictionary<string, string>();

            var queriedValueForPositionData = _positionData.loadPositionData
                                .GroupBy(k => k.fundUniqueIdentifier)
                                .Select(k => new { id = k.Key, totalExposure = k.Sum(x => Math.Abs(Convert.ToDouble(x.exposure))) });
                                
            var queriedValueForFundLevelData = _fundLevelData.loadPositionData
                               .Select(k => new { netAssetValue = Convert.ToDouble(k.netAssetValue), factor = k.leverageGrossMethod, id = k.funduniqueIdentifier });

            var dataJoined = (from positionItem in queriedValueForPositionData
                              join fundItem in queriedValueForFundLevelData on positionItem.id equals fundItem.id
                              select new { factor = fundItem.factor, id = fundItem.id, total = positionItem.totalExposure, nav = fundItem.netAssetValue });

            _answer.Add("ID", "Factor");
            foreach (var element in dataJoined)
            {
                string temp;
                if (element.factor != null)
                    temp = element.factor;
                else
                    temp = Convert.ToString(element.nav / element.total);
                _answer.Add(element.id, temp);
            }

            _writeBldr.writeFile(_answer, "AIF.2.294-295");
        }

        private void question2208()
        {
            _answer = new Dictionary<string, string>();

            var queriedValue = _fundOwnershipData.loadPositionData
                            .Select(k => new { AIF_Investor_Group = k.aIFInvestorGroup, Percentage_Ownership = string.IsNullOrEmpty(k.percentageOwnership) ? 0.0 : double.Parse(k.percentageOwnership)})
                            .GroupBy(k => k.AIF_Investor_Group)
                            .Select(k => new { AIF_Investor_Group = k.Key, sumPercentage_Ownership = k.Sum(x => x.Percentage_Ownership) });


            foreach (var element in queriedValue)
            {
                string temp = (element.AIF_Investor_Group == null ? "NULL" : element.AIF_Investor_Group).ToString();
                if (temp.Contains(','))
                    temp = temp.Replace(',', '|');
                _answer.Add(temp, Convert.ToString(element.sumPercentage_Ownership) );
            }

            _writeBldr.writeFile(_answer, "AIF.2.208-209");



        }

        private void question158()
        {
            _answer = new Dictionary<string, string>();
            int counter = 0;
            bool typeFlag = false;
            string preId = "";

            var queriedPositionValue = _positionData.loadPositionData
                                    .Select(k => new { aIFStrategy = string.IsNullOrEmpty(k.aIFStrategy) ? "No Strategy Provided" : k.aIFStrategy, fundUniqueIdentifier = k.fundUniqueIdentifier, marketValue = string.IsNullOrEmpty(k.localMarketValue) ? 0.0 : Convert.ToDouble(k.localMarketValue) / Convert.ToDouble(k.fXRateBase), businessDate = k.businessDate })
                                    .GroupBy(k => new { k.fundUniqueIdentifier, k.businessDate })
                                    .Select(k => new { id = k.Key, marketValue = k.Sum(x => Convert.ToDouble(x.marketValue)) }).ToList();


            var queriedFundAumData = _fundAumData.loadPositionData
                                    .Select(k => new { id = k.fundUniqueIdentifier, type = k.fundType });


            var queriedPositionFundIndividual = _positionData.loadPositionData
                                               .Select(k => new
                                                                {
                                                                    id = k.fundUniqueIdentifier,
                                                                    value = string.IsNullOrEmpty(k.localMarketValue) ? 0.0 : Convert.ToDouble(k.localMarketValue) / Convert.ToDouble(k.fXRateBase),
                                                                    aIFStrategy = string.IsNullOrEmpty(k.aIFStrategy) ? "No Strategy Provided" : k.aIFStrategy
                                                                }).ToList();


            
            var fundTypeJoinedData = (from positionItem in queriedPositionValue
                                      join fundAumItem in queriedFundAumData on positionItem.id.fundUniqueIdentifier equals fundAumItem.id
                                      select new { id = fundAumItem.id, type = fundAumItem.type }).ToList();
            Dictionary<string,double> dict = new Dictionary<string,double>();

            foreach(var element in queriedPositionValue)
            {
                string tempKey = element.id.fundUniqueIdentifier;
                dict.Add(tempKey,element.marketValue);
            }


            foreach (var element in queriedPositionFundIndividual)
            {
                if (preId != element.id)
                {
                    preId = element.id;
                    typeFlag = true;
                }
                if (typeFlag)
                {
                    if (counter < fundTypeJoinedData.Count())
                        _answer.Add(fundTypeJoinedData[counter].id ,fundTypeJoinedData[counter].type);
                    else
                        _answer.Add(counter.ToString()+ "-No Data", "");
                    typeFlag = false;
                    counter += 1;
                }
                
                string tempKey = element.id + element.aIFStrategy;
                if (_answer.ContainsKey(tempKey))
                    _answer[tempKey] = Convert.ToString(Convert.ToDouble(_answer[tempKey]) + Convert.ToDouble(element.value) / Convert.ToDouble(dict[element.id]));
                else
                    _answer.Add(tempKey, Convert.ToString(Convert.ToDouble(element.value) / Convert.ToDouble(dict[element.id])));
            }

            _writeBldr.writeFile(_answer, "AIF.1.58-61");
        }

    }
}
