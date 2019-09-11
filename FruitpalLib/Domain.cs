using System;
using System.Collections.Generic;

namespace FruitpalLib.Domain
{
    public interface IMarketOverheadProvider {
        IEnumerable<IMarketOverhead> LoadData();
    }

    public interface IMarketOverhead {
        string Country { get; }
        string Commodity { get; }
        double FixedOverhead { get; }
        double VariableOverhead { get; }
        void Deconstruct(out string country, out string commodity, out double fixedOverhead, out double varOverhead);
    }

    public abstract class BaseMarketOverhead: IMarketOverhead {
        public virtual string Country { get; set; }
        public virtual string Commodity {get; set; }
        public virtual double FixedOverhead {get; set; }
        public virtual double VariableOverhead {get; set; }
        public void Deconstruct(out string country, out string commodity, out double fixedOH, out double varOH) {
            country = Country;
            commodity = Commodity;
            fixedOH = FixedOverhead;
            varOH = VariableOverhead;
        }
    }
}
