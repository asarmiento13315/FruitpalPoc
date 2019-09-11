using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FruitpalLib.Domain;

namespace FruitpalTests 
{
  class StubbedMarketOverhead : BaseMarketOverhead { }

  class StubbedMarketOverheadProvider: 
    List<(string country, string commodity, double fixedOverhead, double varOverhead)>, 
    IMarketOverheadProvider 
  {
    public IMarketOverhead GetStub(int id) {
      var s = this[id];
      return new StubbedMarketOverhead { 
          Country = s.country, Commodity = s.commodity, FixedOverhead = s.fixedOverhead, VariableOverhead = s.varOverhead 
        };
    }

    public IEnumerable<IMarketOverhead> LoadData() { 
      return this.Select(s => new StubbedMarketOverhead { 
          Country = s.country, Commodity = s.commodity, FixedOverhead = s.fixedOverhead, VariableOverhead = s.varOverhead 
        });
    }
  }

  class Utils {
    public static void AssertMarketOverheadDataAreEqual(IMarketOverhead x, IMarketOverhead y) {
        Assert.AreEqual(x.Country, y.Country);
        Assert.AreEqual(x.Commodity, y.Commodity);
        Assert.AreEqual(x.FixedOverhead, y.FixedOverhead);
        Assert.AreEqual(x.VariableOverhead, y.VariableOverhead);
    }  
  }

}