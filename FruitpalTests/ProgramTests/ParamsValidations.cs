using System;
using NUnit.Framework;
using Fruitpal;

namespace FruitpalTests 
{
  [TestFixture]
  class ParamsValidations {
    const string missing3ArgsErrMsg = "error: missing arguments\n  run fruitpal <commodity> <price per ton> <trade volume>\n  Ex. run fruitpal mango 53 405";
    const string invalidCommodityNameErrMsg = "error: commodity must be a string -- Ex. mango\nParameter name: arg #1";
    const string invalidTonPriceErrMsg = "error: price per ton must be a number -- Ex. 53\nParameter name: arg #2";
    const string invalidTradeVolumenErrMsg = "error: trade volumen must be an integer number -- Ex. 504\nParameter name: arg #3";

    [TestCase(new string[] {}, missing3ArgsErrMsg, null)]
    [TestCase(new string[] {"x"}, missing3ArgsErrMsg, null)]
    [TestCase(new string[] {"x","y"}, missing3ArgsErrMsg, null)]
    [TestCase(new string[] {"","_","_"}, invalidCommodityNameErrMsg, null)]
    [TestCase(new string[] {"x","y","_"}, invalidTonPriceErrMsg, null)]
    [TestCase(new string[] {"x","-1","_"}, invalidTonPriceErrMsg, null)]
    [TestCase(new string[] {"x","53","z"}, invalidTradeVolumenErrMsg, null)]
    [TestCase(new string[] {"x","53","-1"}, invalidTradeVolumenErrMsg, null)]
    [TestCase(new string[] {"x","53","10.5"}, invalidTradeVolumenErrMsg, null)]
    [TestCase(new string[] {"x","53.5","10"}, null, new object[] {"x", 53.5, 10})]
    public void GetParams_Should_Validate_Args(string[] args, string errMsg, object[] vals) {
      Exception error = null;
      object ps = null;
      try {
        ps = Program.GetParams(args);
      } catch(Exception e) {
        error = e;
      }
      if (errMsg == null) {
        Assert.IsNull(error);
        Assert.AreEqual((vals[0], vals[1], vals[2]), ps);
      } else {
        Assert.IsNotNull(error);
        Assert.AreEqual(errMsg, error.Message);
      }
    }
  }
}