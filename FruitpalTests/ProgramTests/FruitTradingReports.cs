using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FruitpalLib.Domain;
using Fruitpal;

namespace FruitpalTests 
{
  [TestFixture]
  class FruitTradingReports 
  {
    [Test]
    public void Should_Report_Correctly() {
      var stubbedMarketOverheadProvider = new StubbedMarketOverheadProvider();
      stubbedMarketOverheadProvider.Add(("MX", "mango", 32.0, 1.24));
      stubbedMarketOverheadProvider.Add(("CU", "banana", 15.0, 2.00));
      stubbedMarketOverheadProvider.Add(("BR", "mango", 20.0, 1.42));
      var reportLineList = new List<string>();
      var fruitTrading = new FruitTrading(stubbedMarketOverheadProvider, (line) => reportLineList.Add(line));

      fruitTrading.ReportFullCostAcrossCountries("mango", 53, 405);

      CollectionAssert.AreEqual(new [] {
        "BR 22060.10 | (54.42*405)+20",
        "MX 21999.20 | (54.24*405)+32",
      }, reportLineList);
    }
  }
}