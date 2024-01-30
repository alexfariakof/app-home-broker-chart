namespace Domain.Charts.ValueObject;

/// <summary>
/// Representa o objeto de valor SMA (Simple Moving Average) para análise financeira.
/// </summary>
public record Sma
{
    /// <summary>
    /// Obtém ou define a lista de valores da SMA.
    /// </summary>
    public List<decimal> Values { get; set; } = new List<decimal>();

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Sma"/>.
    /// </summary>
    /// <param name="historyPriceData">Os dados históricos de preço usados para o cálculo da SMA.</param>
    /// <param name="periodDays">O número de dias usado no cálculo da SMA (padrão: 5).</param>
    /// <exception cref="ArgumentException">Lançada quando há dados insuficientes para o cálculo da SMA.</exception>
    public Sma(List<decimal> historyPriceData, int periodDays = 5)
    {
        if (historyPriceData == null || historyPriceData.Count == 0 || historyPriceData.Count < periodDays)
            throw new ArgumentException("Não há dados suficientes para gerar uma SMA.");

        var count = historyPriceData.Count;
        for (int i = 0; i < count; i++)
        {
            decimal sum = 0;
            for (int j = 0; j < periodDays; j++)
            {
                if (i + j >= count)
                {
                    if (i > periodDays + 1)
                    {
                        sum = Values.Last() * periodDays;
                    }
                    break;
                }
                sum += historyPriceData[i + j];
            }
            decimal average = sum / periodDays;
            Values.Add(average);
        }
    }
}
