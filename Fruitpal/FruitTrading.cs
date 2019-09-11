using System;
using FruitpalLib;
using FruitpalLib.Domain;

namespace Fruitpal
{
    public class FruitTrading {
        private IMarketOverheadProvider marketOverheadProvider;
        private Action<string> textLineReporter;

        public FruitTrading(IMarketOverheadProvider marketOverheadProvider, Action<string> textLineReporter) {
            if (marketOverheadProvider is null)
                throw new ArgumentNullException(nameof(marketOverheadProvider));
            if (textLineReporter is null)
                throw new ArgumentNullException(nameof(textLineReporter));
            this.marketOverheadProvider = marketOverheadProvider;
            this.textLineReporter = textLineReporter;
        }

        public void ReportFullCostAcrossCountries(string commodity, double tonPrice, double tradeVolume) {
            var trader = new FruitTrader(marketOverheadProvider);

            var matches = trader.QueryFullCostAcrossCountries(commodity, tonPrice, tradeVolume);

            foreach(var m in matches) {
                var (poundCost, totalCost, marketOverheadData) = m;
                var (country, _, fixedOverhead, _) = marketOverheadData;
                textLineReporter($"{country} {totalCost,8:F2} | ({poundCost:F2}*{tradeVolume})+{fixedOverhead}");
            }
        }
    }
}