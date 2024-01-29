namespace Domain.Charts.ValueObject;
public record Period
{    
    private readonly DateTime MINIMUN_DATE = new DateTime(2011, 5, 2);
    public static implicit operator Period((DateTime start, DateTime end) value) => new Period { StartDate = value.start, EndDate = value.end };
    public static implicit operator (DateTime start, DateTime end)(Period period) => (period.StartDate, period.EndDate);
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Days => (int)(EndDate - StartDate).TotalDays;
    private Period() { }
    public Period(DateTime startDate, DateTime endDate)
    {

        // Verificar se a data final é maior ou igual à data inicial
        if (endDate <= startDate)
            throw new ArgumentException("A data final deve ser maior que à data inicial.");

        // Verificar se o intervalo entre as datas é pelo menos 5 dias
        if ((endDate - startDate).TotalDays < 5)
            throw new ArgumentException("O intervalo entre as datas deve ser de pelo menos 5 dias.");

        // Verificar se a data não é anterior a "2011-05-02"        
        if (startDate < MINIMUN_DATE || endDate < MINIMUN_DATE)
            throw new ArgumentException("A data não deve ser anterior a '02/05/2011'.");

        StartDate = startDate;
        EndDate = endDate;
    }
}