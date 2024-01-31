using Business.Interfaces;
using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;
using Microsoft.AspNetCore.Mvc;

namespace HomeBrokerSPA.Controllers;

/// <summary>
/// Controller responsável por fornecer dados relacionados aos gráficos para o Home Broker.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ChartHomeBrokerController : ControllerBase
{
    private readonly IHomeBrokerBusiness _homeBrokerBusiness;

    /// <summary>
    /// Inicializa uma nova instância do controlador <see cref="ChartHomeBrokerController"/>.
    /// </summary>
    /// <param name="homeBrokerBusiness">A instância da interface de negócios do Home Broker.</param>
    public ChartHomeBrokerController(IHomeBrokerBusiness homeBrokerBusiness)
    {
        _homeBrokerBusiness = homeBrokerBusiness ?? throw new ArgumentNullException(nameof(homeBrokerBusiness));
    }

    /// <summary>
    /// Obtém os dados históricos de preço para o período especificado.
    /// </summary>
    /// <param name="StartDate">A data de início do período.</param>
    /// <param name="EndDate">A data de término do período.</param>
    /// <returns>Os dados históricos de preço para o período especificado.</returns>
    [HttpGet("{StartDate}/{EndDate}")]
    [ProducesResponseType((200), Type = typeof(List<MagazineLuizaHistoryPrice>))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((204))]
    public IActionResult Get([FromRoute] DateTime StartDate, [FromRoute] DateTime EndDate)
    {
        try
        {
            var period = new Period(StartDate, EndDate);
            var result = _homeBrokerBusiness.GetHistoryData(period);
            return Ok(result);
        }
        catch 
        {
            return NoContent();
        }
    }

    /// <summary>
    /// Obtém a Média Móvel Simples (SMA) para o período especificado.
    /// </summary>
    /// <param name="StartDate">A data de início do período.</param>
    /// <param name="EndDate">A data de término do período.</param>
    /// <returns>A Média Móvel Simples (SMA) para o período especificado.</returns>
    [HttpGet("GetSMA/{StartDate}/{EndDate}")]
    [ProducesResponseType((200), Type = typeof(Sma))]
    [ProducesResponseType((400), Type = typeof(string))]
    public IActionResult GetSMA([FromRoute] DateTime StartDate, [FromRoute] DateTime EndDate)
    {
        try
        {
            var period = new Period(StartDate, EndDate);
            var result = _homeBrokerBusiness.GetSMA(period);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Obtém a Média Móvel Exponencial (EMA) para o período e número de dias especificados.
    /// </summary>
    /// <param name="PeriodDays">O número de dias para o cálculo da EMA.</param>
    /// <param name="StartDate">A data de início do período.</param>
    /// <param name="EndDate">A data de término do período.</param>
    /// <returns>A Média Móvel Exponencial (EMA) para o período e número de dias especificados.</returns>
    [HttpGet("GetEMA/{PeriodDays}/{StartDate}/{EndDate}")]
    [ProducesResponseType((200), Type = typeof(Ema))]
    [ProducesResponseType((400), Type = typeof(string))]
    public IActionResult GetEMA([FromRoute] int PeriodDays, [FromRoute] DateTime StartDate, [FromRoute] DateTime EndDate)
    {
        try
        {
            var period = new Period(StartDate, EndDate);
            return Ok(_homeBrokerBusiness.GetEMA(PeriodDays, period));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Obtém o Moving Average Convergence Divergence (MACD) para o período especificado.
    /// </summary>
    /// <param name="StartDate">A data de início do período.</param>
    /// <param name="EndDate">A data de término do período.</param>
    /// <returns>O Moving Average Convergence Divergence (MACD) para o período especificado.</returns>
    [HttpGet("GetMACD/{StartDate}/{EndDate}")]
    [ProducesResponseType((200), Type = typeof(MACD))]
    [ProducesResponseType((400), Type = typeof(string))]
    public IActionResult GetMACD([FromRoute] DateTime StartDate, [FromRoute] DateTime EndDate)
    {
        try
        {
            var period = new Period(StartDate, EndDate);
            return Ok(_homeBrokerBusiness.GetMACD(period));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
