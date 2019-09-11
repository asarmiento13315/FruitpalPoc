using System;
using System.Collections.Generic;
using System.Linq;
using FruitpalLib.Domain;

namespace FruitpalLib
{
    public class FruitTrader {
        private readonly IMarketOverheadProvider marketOverheadProvider;

        public FruitTrader(IMarketOverheadProvider marketOverheadProvider) {
            if (marketOverheadProvider is null)
                throw new ArgumentNullException(nameof(marketOverheadProvider));
            this.marketOverheadProvider = marketOverheadProvider;
        }

        public IEnumerable<(double poundCost, double totalCost, IMarketOverhead marketOverheadData)> QueryFullCostAcrossCountries(string commodity, double tonPrice, double tradeVolume) {
            var matches = marketOverheadProvider.LoadData()
                .Where((x) => x.Commodity == commodity)
                .Select((x) => {
                    var poundCost = tonPrice + x.VariableOverhead;
                    var totalCost = Math.Round(poundCost * tradeVolume + x.FixedOverhead, 2);
                    return (
                        poundCost,
                        totalCost,
                        marketOverheadData: x
                    );
                })
                .OrderByDescending((x) => x.totalCost);
            return matches;
        }
    }
}
