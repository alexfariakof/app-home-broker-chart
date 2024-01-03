namespace Domain.Charts.ValueObject;
public record EMA
{
    public List<decimal> Values { get; set; } = new List<decimal>();
    public EMA(List<decimal> historyPriceData, int periodDays)
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