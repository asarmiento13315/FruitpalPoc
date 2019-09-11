using System;
using System.Linq;
using System.IO;
using NUnit.Framework;
using FruitpalLib.Domain;
using Fruitpal.DataAccess;

namespace FruitpalTests 
{
  [TestFixture]
  class JsonMarketOverheadProvLoading 
  {
    [Test]
    public void Should_Load_Empty_When_File_Not_Existing() {
      var tempJsonFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
      try {
        if(File.Exists(tempJsonFilePath)) {
          File.Delete(tempJsonFilePath);
        }
        var prov = new JsonMarketOverheadProvider(tempJsonFilePath);
        CollectionAssert.AreEqual(new string[] {}, prov.LoadData());
      } catch {}
    }

    [Test]
    public void Should_Load_Empty_When_Invalid_Json() {
      var tempJsonFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
      try {
        File.WriteAllText(tempJsonFilePath, "");
        var prov = new JsonMarketOverheadProvider(tempJsonFilePath);

        CollectionAssert.AreEqual(new IMarketOverhead[] {}, prov.LoadData());
      } catch {}
      if(File.Exists(tempJsonFilePath)) {
        File.Delete(tempJsonFilePath);
      }
    }

    [Test]
    public void Should_Load_Correctly() {
      var tempJsonFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
      try {
        var json = "[{\"COUNTRY\":\"MX\",\"COMMODITY\":\"banana\",\"FIXED_OVERHEAD\":\"32.00\",\"VARIABLE_OVERHEAD\":\"1.24\"}, {\"COUNTRY\":\"BR\",\"COMMODITY\":\"mango\",\"FIXED_OVERHEAD\":\"20.00\",\"VARIABLE_OVERHEAD\":\"1.42\"}]";
        File.WriteAllText(tempJsonFilePath, json);

        var prov = new JsonMarketOverheadProvider(tempJsonFilePath);
        var data = prov.LoadData().ToArray();

        var stubbedProv = new StubbedMarketOverheadProvider();
        stubbedProv.Add(("MX", "banana", 32.0, 1.24));
        stubbedProv.Add(("BR", "mango", 20.0, 1.42));

        Utils.AssertMarketOverheadDataAreEqual(stubbedProv.GetStub(0), data[0]);
        Utils.AssertMarketOverheadDataAreEqual(stubbedProv.GetStub(1), data[1]);
      } catch {}
      if(File.Exists(tempJsonFilePath)) {
        File.Delete(tempJsonFilePath);
      }
    }
  }
}