namespace Domain.Charts.ValueObject;
public record Period
{
    public static implicit operator Period((DateTime start, DateTime end) value) => new Period { StartDate = value.start, EndDate = value.end };
    public static implicit operator (DateTime start, DateTime end)(Period period) => (period.StartDate, period.EndDate);
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Days => (int)(EndDate - StartDate).TotalDays;
    private Period() { }
    public Period(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}