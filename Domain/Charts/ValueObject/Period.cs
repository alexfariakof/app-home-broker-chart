namespace Domain.Charts.ValueObject;

/// <summary>
/// Representa o Object Value Periodo para definir um intervalo de datas.
/// </summary>
public record Period
{
    private readonly DateTime MINIMUM_DATE = new DateTime(2011, 5, 2);
    /// <summary>
    /// Converte implicitamente um valor de tupla (Data de início, Data de término) para um objeto Periodo.
    /// </summary>
    public static implicit operator Period((DateTime start, DateTime end) value) => new Period { StartDate = value.start, EndDate = value.end };

    /// <summary>
    /// Obtém ou define a data de início do período.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Obtém ou define a data de término do período.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Obtém a quantidade de dias no período.
    /// </summary>
    public int Days => (int)(EndDate - StartDate).TotalDays;

    /// <summary>
    /// Método Privado que inicializa uma nova instância da classe <see cref="Period"/>.
    /// </summary>
    private Period() { }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Period"/>.
    /// </summary>
    /// <param name="startDate">A data de início do período.</param>
    /// <param name="endDate">A data de término do período.</param>
    /// <exception cref="ArgumentException">Lançada quando a data final é menor ou igual à data inicial,
    /// o intervalo entre as datas é menor que 5 dias ou a data é anterior a '02/05/2011'.</exception>
    public Period(DateTime startDate, DateTime endDate)
    {
        // Verificar se a data final é maior ou igual à data inicial
        if (endDate <= startDate)
            throw new ArgumentException("A data final deve ser maior que à data inicial.");

        // Verificar se o intervalo entre as datas é pelo menos 5 dias
        if ((endDate - startDate).TotalDays < 5)
            throw new ArgumentException("O intervalo entre as datas deve ser de pelo menos 5 dias.");

        // Verificar se a data não é anterior a "2011-05-02"        
        if (startDate < MINIMUM_DATE || endDate < MINIMUM_DATE)
            throw new ArgumentException("A data não deve ser anterior a '02/05/2011'.");

        StartDate = startDate;
        EndDate = endDate;
    }

}