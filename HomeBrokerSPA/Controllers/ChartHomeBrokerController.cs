using Business.Interfaces;
using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;
using Microsoft.AspNetCore.Mvc;

namespace HomeBrokerSPA.Controllers;

[ApiController]
[Route("[controller]")]
public class ChartHomeBrokerController : ControllerBase
{
    private readonly IHomeBrokerBusiness _homeBrokerBusiness;

    public ChartHomeBrokerController(IHomeBrokerBusiness homeBrokerBusiness)
    {
        _homeBrokerBusiness = homeBrokerBusiness;
    }

    [HttpGet("{StartDate}/{EndDate}")]
    [ProducesResponseType((200), Type = typeof(List<MagazineLuizaHistoryPrice>))]
    [ProducesResponseType((400), Type = typeof(String))]
    public IActionResult Get([FromRoute] DateTime StartDate, [FromRoute] DateTime EndDate)
    {        
        try
        {
            var period = new Period(StartDate, EndDate);
            var result = _homeBrokerBusiness.GetHistoryData(period);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Erro desconhecido, tente novamente mais tarde."} );
        }
    }

    [HttpGet("GetSMA/{StartDate}/{EndDate}")]
    [ProducesResponseType((200), Type = typeof(Sma))]
    [ProducesResponseType((400), Type = typeof(String))]
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

    [HttpGet("GetEMA/{PeriodDays}/{StartDate}/{EndDate}")]
    [ProducesResponseType((200), Type = typeof(Ema))]
    [ProducesResponseType((400), Type = typeof(String))]
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

    [HttpGet("GetMACD/{StartDate}/{EndDate}")]
    [ProducesResponseType((200), Type = typeof(MACD))]
    [ProducesResponseType((400), Type = typeof(String))]
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