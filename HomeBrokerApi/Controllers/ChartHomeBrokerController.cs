using Business.Interfaces;
using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;
using Microsoft.AspNetCore.Mvc;

namespace HomeBrokerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ChartHomeBrokerController : ControllerBase
{
    private readonly IHomeBrokerBusiness _homeBrokerBusiness;
    
    public ChartHomeBrokerController(IHomeBrokerBusiness homeBrokerBusiness)
    {
        _homeBrokerBusiness = homeBrokerBusiness;
    }

    [HttpGet]
    public List<MagazineLuizaHistoryPrice> Get([FromQuery] DateTime StartDate, [FromQuery] DateTime EndDate)
    {
        var period = new Period(StartDate, EndDate);
        var result = _homeBrokerBusiness.GetHistoryData(period);
        return result;
    }

    [HttpGet("GetSMA")]
    public Sma GetSMA()
    {
        return _homeBrokerBusiness.GetSMA();
    }

    [HttpGet("GetEMA/{PeriodDays}")]
    public Ema GetEMA([FromRoute] int PeriodDays)
    {
        return _homeBrokerBusiness.GetEMA(PeriodDays);
    }
}