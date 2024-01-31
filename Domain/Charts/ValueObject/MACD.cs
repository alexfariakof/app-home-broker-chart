namespace Domain.Charts.ValueObject;

/// <summary>
/// Representa o objeto de valor MACD (Moving Average Convergence Divergence) para análise financeira.
/// </summary>
public record MACD
{
    private const int MININUM_AMOUNT_DATA = 34;
    private const int MACD_LINE_VALUE = 9;
    private const int EMA12_VALUE = 12;
    private const int EMA26_VALUE = 26;

    /// <summary>
    /// Obtém ou define a linha MACD (12-day EMA - 26-day EMA).
    /// </summary>
    public List<decimal> MACDLine { get; set; } = new List<decimal>();

    /// <summary>
    /// Obtém ou define a linha de sinal (9-day EMA da linha MACD).
    /// </summary>
    public List<decimal> Signal { get; set; } = new List<decimal>();

    /// <summary>
    /// Obtém ou define o histograma MACD (MACD Line - Signal Line).
    /// </summary>
    public List<decimal> Histogram { get; set; } = new List<decimal>();

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="MACD"/>.
    /// </summary>
    /// <param name="historyPrice">Os dados históricos de preço usados para o cálculo do MACD.</param>
    /// <exception cref="ArgumentException">Lançada quando há dados insuficientes para o cálculo do MACD.</exception>
    public MACD(List<decimal> historyPrice)
    {
        if (historyPrice == null || historyPrice.Count == 0 || historyPrice.Count < MININUM_AMOUNT_DATA)
            throw new ArgumentException("Não há dados suficientes para gerar um MACD.");

        var ema12 = new Ema(historyPrice, EMA12_VALUE);
        var ema26 = new Ema(historyPrice, EMA26_VALUE);

        // Linha MACD: (12-day EMA - 26-day EMA)
        for (var i = 0; i < ema12.Values.Count; i++)
        {
            if (i < ema26.Values.Count)
                MACDLine.Add(ema12.Values[i] - ema26.Values[i]);
        }

        // Linha de Sinal: 9-day EMA da Linha MACD
        for (var i = 0; i < MACDLine.Count; i++)
            Signal = new Ema(MACDLine, MACD_LINE_VALUE).Values;

        // Histograma MACD: Linha MACD - Linha de Sinal
        for (var i = 0; i < historyPrice.Count; i++)
        {
            if (i < Signal.Count)
                Histogram.Add(MACDLine[i] - Signal[i]);
        }
    }
}
