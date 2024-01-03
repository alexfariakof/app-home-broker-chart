namespace Domain.Charts.ValueObject;
public record EMA
{
    public List<decimal> Values { get; set; } = new List<decimal>();
}