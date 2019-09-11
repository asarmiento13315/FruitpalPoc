using Microsoft.Extensions.Configuration;
using System;
using Fruitpal.DataAccess;

namespace Fruitpal
{
    public class Program
    {
        static string MarketOverheadJsonFileKey = "marketOverheadJsonFile";
        static IConfiguration Config { get; } = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        public static void Main(string[] args)
        {
            try {
                var (commodity, tonPrice, tradeVolume) = GetParams(args);

                var marketOverheadJsonFile = Config[MarketOverheadJsonFileKey];
                var jsonMarketOverheadProvider = new JsonMarketOverheadProvider(marketOverheadJsonFile);
                var fruitTrading = new FruitTrading(jsonMarketOverheadProvider, (line) => Console.WriteLine(line));

                fruitTrading.ReportFullCostAcrossCountries(commodity, tonPrice, tradeVolume);
            } catch(Exception exc) {
                Console.WriteLine(exc.Message);
            }
        }

        public static (string commodity, double tonPrice, int tradeVolume) GetParams(string[] args) {
            if (args.Length < 3)
                throw new ArgumentException("error: missing arguments\n  run fruitpal <commodity> <price per ton> <trade volume>\n  Ex. run fruitpal mango 53 405");
            var commodity = args[0];
            if (string.IsNullOrEmpty(commodity))
                throw new ArgumentException("error: commodity must be a string -- Ex. mango", "arg #1");
            var tonPrice = 0.0;
            if (!double.TryParse(args[1], out tonPrice) || tonPrice <= 0.0) 
                throw new ArgumentException("error: price per ton must be a number -- Ex. 53", "arg #2");
            var tradeVolume = 0;
            if (!int.TryParse(args[2], out tradeVolume) || tradeVolume <= 0) 
                throw new ArgumentException("error: trade volumen must be an integer number -- Ex. 504", "arg #3");
            return (commodity, tonPrice, tradeVolume);
        }
    }
}
