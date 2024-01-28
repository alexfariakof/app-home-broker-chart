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
    public List<MagazineLuizaHistoryPrice> Get([FromRoute] DateTime StartDate, [FromRoute] DateTime EndDate)
    {
        var period = new Period(StartDate, EndDate);
        var result = _homeBrokerBusiness.GetHistoryData(period);
        return result;
    }

    [HttpGet("GetSMA/{StartDate}/{EndDate}")]
    public Sma GetSMA([FromRoute] DateTime StartDate, [FromRoute] DateTime EndDate)
    {
        var period = new Period(StartDate, EndDate);
        return _homeBrokerBusiness.GetSMA(period);
    }

    [HttpGet("GetEMA/{PeriodDays}/{StartDate}/{EndDate}")]
    public Ema GetEMA([FromRoute] int PeriodDays, [FromRoute] DateTime StartDate, [FromRoute] DateTime EndDate)
    {
        var period = new Period(StartDate, EndDate);
        return _homeBrokerBusiness.GetEMA(PeriodDays, period);
    }

    [HttpGet("GetMACD/{StartDate}/{EndDate}")]
    public MACD GetMACD([FromRoute] DateTime StartDate, [FromRoute] DateTime EndDate)
    {
        var period = new Period(StartDate, EndDate);
        return _homeBrokerBusiness.GetMACD(period);
    }
}