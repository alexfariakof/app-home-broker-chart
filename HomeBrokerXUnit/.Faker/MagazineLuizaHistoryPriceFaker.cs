using Domain.Charts.Agreggates;
using Bogus;

namespace HomeBrokerXUnit.Faker;

/// <summary>
/// Classe responsável por gerar dados falsos (fakes) para a entidade MagazineLuizaHistoryPrice.
/// </summary>
public class MagazineLuizaHistoryPriceFaker
{
    /// <summary>
    /// Gera uma nova instância falsa de MagazineLuizaHistoryPrice.
    /// </summary>
    /// <returns>Uma instância falsa de MagazineLuizaHistoryPrice.</returns>
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

    /// <summary>
    /// Gera uma lista de instâncias falsas de MagazineLuizaHistoryPrice.
    /// </summary>
    /// <param name="count">O número de instâncias a serem geradas na lista.</param>
    /// <returns>Uma lista de instâncias falsas de MagazineLuizaHistoryPrice.</returns>
    public static List<MagazineLuizaHistoryPrice> GetListFaker(int count)
    {
        List<MagazineLuizaHistoryPrice> listFaker = new List<MagazineLuizaHistoryPrice>();

        for (int i = 0; i < count; i++)
            listFaker.Add(GetNewFaker());

        return listFaker;
    }
}
