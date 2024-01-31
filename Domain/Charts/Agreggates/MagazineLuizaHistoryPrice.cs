using CsvHelper.Configuration.Attributes;

namespace Domain.Charts.Agreggates;
/// <summary>
/// Representa os dados históricos de preço para a Magazine Luiza.
/// </summary>
public class MagazineLuizaHistoryPrice
{
    /// <summary>
    /// Obtém ou define a data para a qual o preço histórico está registrado.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Obtém ou define o preço de abertura na data registrada.
    /// </summary>
    public decimal Open { get; set; }

    /// <summary>
    /// Obtém ou define o preço mais alto atingido na data registrada.
    /// </summary>
    public decimal High { get; set; }

    /// <summary>
    /// Obtém ou define o preço mais baixo atingido na data registrada.
    /// </summary>
    public decimal Low { get; set; }

    /// <summary>
    /// Obtém ou define o preço de fechamento na data registrada.
    /// </summary>
    public decimal Close { get; set; }

    /// <summary>
    /// Obtém ou define o preço de fechamento ajustado na data registrada.
    /// </summary>
    [Name("Adj Close")]
    public double AdjClose { get; set; }

    /// <summary>
    /// Obtém ou define o volume de negociação na data registrada.
    /// </summary>
    public long Volume { get; set; }
}
