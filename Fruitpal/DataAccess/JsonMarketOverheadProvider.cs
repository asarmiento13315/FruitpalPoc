using System;
using System.Collections.Generic;
using System.IO;
using FruitpalLib.Domain;
using Newtonsoft.Json;

namespace Fruitpal.DataAccess {
  public class JsonMarketOverhead: BaseMarketOverhead {
    [JsonProperty("COUNTRY")]
    public override string Country {get; set;}
    [JsonProperty("COMMODITY")]
    public override string Commodity {get; set;}
    [JsonProperty("FIXED_OVERHEAD")]
    public override double FixedOverhead {get; set;}
    [JsonProperty("VARIABLE_OVERHEAD")]
    public override double VariableOverhead {get; set;}
  }

  public class JsonMarketOverheadProvider: IMarketOverheadProvider {
    private readonly string jsonFilename;

    public JsonMarketOverheadProvider(string jsonFilename) {
      if (jsonFilename is null)
        throw new ArgumentNullException(nameof(jsonFilename));
      this.jsonFilename = jsonFilename;
    }

    public IEnumerable<IMarketOverhead> LoadData() {
      var empty = new IMarketOverhead[] {};
      try {
        var json = File.ReadAllText(jsonFilename);
        return JsonConvert.DeserializeObject<JsonMarketOverhead[]>(json) ?? empty;
      } catch {
        return empty;
      }
    }
  }
}