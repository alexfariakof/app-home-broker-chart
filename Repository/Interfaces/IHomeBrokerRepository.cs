using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;

namespace Repository.Interfaces;

/// <summary>
/// Interface que define operações para obter dados históricos do Magazine Luiza.
/// </summary>
public interface IHomeBrokerRepository
{
    /// <summary>
    /// Obtém os dados históricos de preço do Magazine Luiza para o período especificado.
    /// </summary>
    /// <param name="period">O período para o qual os dados históricos são desejados.</param>
    /// <returns>Uma lista de objetos <see cref="MagazineLuizaHistoryPrice"/>.</returns>
    Task<List<MagazineLuizaHistoryPrice>> GetHistoryData(Period period);
}
