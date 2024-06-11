using Business.Interfaces;
using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;
using Microsoft.AspNetCore.Mvc;

namespace HomeBrokerSPA.Controllers;

/// <summary>
/// Controller respons�vel por fornecer dados relacionados aos gr�ficos para o Home Broker.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ChartHomeBrokerController : ControllerBase
{
    private readonly IHomeBrokerBusiness _homeBrokerBusiness;

    /// <summary>
    /// Inicializa uma nova inst�ncia do controlador <see cref="ChartHomeBrokerController"/>.
    /// </summary>
    /// <param name="homeBrokerBusiness">A inst�ncia da interface de neg�cios do Home Broker.</param>
    public ChartHomeBrokerController(IHomeBrokerBusiness homeBrokerBusiness)
    {
        _homeBrokerBusiness = homeBrokerBusiness;
    }

    /// <summary>
    /// Obt�m os dados hist�ricos de pre�o para o per�odo especificado.
    /// </summary>
    /// <param name="StartDate">A data de in�cio do per�odo.</param>
    /// <param name="EndDate">A data de t�rmino do per�odo.</param>
    /// <returns>Os dados hist�ricos de pre�o para o per�odo especificado.</returns>
    [HttpGet("{StartDate}/{EndDate}")]
    [ProducesResponseType((200), Type = typeof(List<MagazineLuizaHistoryPrice>))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((204))]
    public async Task<IActionResult> Get([FromRoute] DateTime StartDate, [FromRoute] DateTime EndDate)
    {
        try
        {
            var period = new Period(StartDate, EndDate);
            var result = await _homeBrokerBusiness.GetHistoryData(period);
            return Ok(result);
        }
        catch
        {
            return NoContent();
        }
    }

    /// <summary>
    /// Obt�m a M�dia M�vel Simples (SMA) para o per�odo especificado.
    /// </summary>
    /// <param name="StartDate">A data de in�cio do per�odo.</param>
    /// <param name="EndDate">A data de t�rmino do per�odo.</param>
    /// <returns>A M�dia M�vel Simples (SMA) para o per�odo especificado.</returns>
    [HttpGet("GetSMA/{StartDate}/{EndDate}")]
    [ProducesResponseType((200), Type = typeof(Sma))]
    [ProducesResponseType((400), Type = typeof(string))]
    public async Task<IActionResult> GetSMA([FromRoute] DateTime StartDate, [FromRoute] DateTime EndDate)
    {
        try
        {
            var period = new Period(StartDate, EndDate);
            var result = await _homeBrokerBusiness.GetSMA(period);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Obt�m a M�dia M�vel Exponencial (EMA) para o per�odo e n�mero de dias especificados.
    /// </summary>
    /// <param name="PeriodDays">O n�mero de dias para o c�lculo da EMA.</param>
    /// <param name="StartDate">A data de in�cio do per�odo.</param>
    /// <param name="EndDate">A data de t�rmino do per�odo.</param>
    /// <returns>A M�dia M�vel Exponencial (EMA) para o per�odo e n�mero de dias especificados.</returns>
    [HttpGet("GetEMA/{PeriodDays}/{StartDate}/{EndDate}")]
    [ProducesResponseType((200), Type = typeof(Ema))]
    [ProducesResponseType((400), Type = typeof(string))]
    public async Task<IActionResult> GetEMA([FromRoute] int PeriodDays, [FromRoute] DateTime StartDate, [FromRoute] DateTime EndDate)
    {
        try
        {
            var period = new Period(StartDate, EndDate);
            var result = await _homeBrokerBusiness.GetEMA(PeriodDays, period);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Obt�m o Moving Average Convergence Divergence (MACD) para o per�odo especificado.
    /// </summary>
    /// <param name="StartDate">A data de in�cio do per�odo.</param>
    /// <param name="EndDate">A data de t�rmino do per�odo.</param>
    /// <returns>O Moving Average Convergence Divergence (MACD) para o per�odo especificado.</returns>
    [HttpGet("GetMACD/{StartDate}/{EndDate}")]
    [ProducesResponseType((200), Type = typeof(MACD))]
    [ProducesResponseType((400), Type = typeof(string))]
    public async Task<IActionResult> GetMACD([FromRoute] DateTime StartDate, [FromRoute] DateTime EndDate)
    {
        try
        {
            var period = new Period(StartDate, EndDate);
            var result = await _homeBrokerBusiness.GetMACD(period);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Gera e baixa um arquivo Excel com dados hist�ricos de pre�o para o per�odo especificado.
    /// </summary>
    /// <param name="StartDate">A data de in�cio do per�odo.</param>
    /// <param name="EndDate">A data de t�rmino do per�odo.</param>
    /// <returns>O arquivo Excel com os dados hist�ricos de pre�o.</returns>
    [HttpGet("DownloadHistory/{StartDate}/{EndDate}")]
    [ProducesResponseType(200, Type = typeof(FileResult))]
    [ProducesResponseType(400, Type = typeof(object))]
    public async Task<IActionResult> DownloadHistory([FromRoute] DateTime StartDate, [FromRoute] DateTime EndDate)
    {
        try
        {
            var period = new Period(StartDate, EndDate);
            var stream = await _homeBrokerBusiness.GenerateExcelHistory(period);

            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = $"History_{StartDate:yyyyMMdd}_{EndDate:yyyyMMdd}.xlsx";

            return File(stream, contentType, fileName);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}