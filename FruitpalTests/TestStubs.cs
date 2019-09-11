using System;
using System.Collections.Generic;
using System.Linq;
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

}