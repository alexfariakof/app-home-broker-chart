namespace Domain.Charts.ValueObject;
public record SMA
{
    public List<decimal> Values { get; set; } = new List<decimal>();
}

