namespace Domain.Charts.ValueObject;

/// <summary>
/// Representa o Object Value Exponential Moving Average (EMA) para análise financeira.
/// </summary>
public record Ema
{
    /// <summary>
    /// Obtém ou define a lista de valores EMA.
    /// </summary>
    public List<decimal> Values { get; set; } = new List<decimal>();

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Ema"/>.
    /// </summary>
    /// <param name="historyPriceData">Os dados históricos de preço usados para o cálculo da EMA.</param>
    /// <param name="periodDays">O número de dias usados no cálculo da EMA.</param>
    /// <exception cref="ArgumentException">Lançada quando há dados insuficientes para o cálculo da EMA.</exception>
    public Ema(List<decimal> historyPriceData, int periodDays)
    {
        if (historyPriceData == null || historyPriceData.Count == 0 || historyPriceData.Count < periodDays)
            throw new ArgumentException("Não há dados suficientes para gerar uma EMA.");

        // Passo 1: Calcular a SMA inicial
        decimal sma = historyPriceData.Take(periodDays).Average();

        // Passo 2: Calcular o multiplicador de ponderação
        decimal multiplier = 2.0m / (periodDays + 1);

        // Adicionar a SMA inicial como o primeiro valor do EMA
        Values.Add(sma);

        // Passo 3: Calcular o EMA para os dias subsequentes
        for (int i = periodDays; i < historyPriceData.Count; i++)
        {
            decimal currentPrice = historyPriceData[i];
            decimal ema = (currentPrice - Values.Last()) * multiplier + Values.Last();
            Values.Add(ema);
        }
    }
}
