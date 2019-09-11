using System.Linq;
using NUnit.Framework;
using FruitpalLib;

namespace FruitpalTests 
{
  [TestFixture]
  class TraderQuery 
  {
    [Test]
    public void Should_Query_Correctly() {
      var stubbedMarketOverheadProvider = new StubbedMarketOverheadProvider();
      stubbedMarketOverheadProvider.Add(("MX", "mango", 32.0, 1.24));
      stubbedMarketOverheadProvider.Add(("CU", "banana", 15.0, 2.00));
      stubbedMarketOverheadProvider.Add(("BR", "mango", 20.0, 1.42));

      var fruitTrader = new FruitTrader(stubbedMarketOverheadProvider);

      var matches = fruitTrader.QueryFullCostAcrossCountries("mango", 53, 405).ToArray();

      Assert.That(matches, Has.Exactly(2).Items);

      Assert.AreEqual(54.42, matches[0].poundCost);
      Assert.AreEqual(22060.10, matches[0].totalCost);
      Utils.AssertMarketOverheadDataAreEqual(stubbedMarketOverheadProvider.GetStub(2), matches[0].marketOverheadData);

      Assert.AreEqual(54.24, matches[1].poundCost);
      Assert.AreEqual(21999.20, matches[1].totalCost);
      Utils.AssertMarketOverheadDataAreEqual(stubbedMarketOverheadProvider.GetStub(0), matches[1].marketOverheadData);
    }

    [Test]
    public void Should_Query_Correctly_When_No_Match() {
      var stubbedMarketOverheadProvider = new StubbedMarketOverheadProvider();
      stubbedMarketOverheadProvider.Add(("MX", "mango", 32.0, 1.24));
      stubbedMarketOverheadProvider.Add(("CU", "banana", 15.0, 2.00));
      stubbedMarketOverheadProvider.Add(("BR", "mango", 20.0, 1.42));

      var fruitTrader = new FruitTrader(stubbedMarketOverheadProvider);

      var matches = fruitTrader.QueryFullCostAcrossCountries("apple", 53, 405).ToArray();

      Assert.That(matches, Has.Exactly(0).Items);
    }
  }
}