using Bogus;
using Domain.Charts.Agreggates;

namespace HomeBrokerXUnit.Faker;
public class MagazineLuizaHistoryPriceFaker
{
    public static MagazineLuizaHistoryPrice GetNewFaker()
    {
        return new Faker<MagazineLuizaHistoryPrice>()
            .RuleFor(p => p.Date, f => f.Date.Recent())
            .RuleFor(p => p.Open, f => f.Finance.Amount(100, 1000))
            .RuleFor(p => p.High, f => f.Finance.Amount(100, 1000))
            .RuleFor(p => p.Low, f => f.Finance.Amount(100, 1000))
            .RuleFor(p => p.Close, f => f.Finance.Amount(100, 1000))
            .RuleFor(p => p.AdjClose, f => f.Random.Double(50, 150))
            .RuleFor(p => p.Volume, f => f.Random.Long(1000, 10000)).Generate();
    }
    public static List<MagazineLuizaHistoryPrice> GetListFaker(int count)
    {
        List<MagazineLuizaHistoryPrice> listFaker = new List<MagazineLuizaHistoryPrice>();

        for(int i=0; i <=count; i++)
            listFaker.Add(GetNewFaker());

        return listFaker;
    }
}