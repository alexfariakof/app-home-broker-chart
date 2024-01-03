using CsvHelper.Configuration.Attributes;

namespace Domain.Charts.Agreggates;
public class MagazineLuizaHistoryPrice
{
    public DateTime Date { get; set; }
    public decimal Open { get; set; }
    public decimal  High { get; set; }
    public decimal  Low { get; set; }
    public decimal  Close { get; set; }
    [Name("Adj Close")]
    public double  AdjClose { get; set; }
    public long Volume { get; set; }
}