using Business.Interfaces;
using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ChartHomeBrokerController : ControllerBase
{
    private readonly ILogger<ChartHomeBrokerController> _logger;
    private IHomeBrokerBusiness _homeBrokerBusiness;
    public ChartHomeBrokerController(ILogger<ChartHomeBrokerController> logger, IHomeBrokerBusiness homeBrokerBusiness)
    {
        _logger = logger;
        _homeBrokerBusiness = homeBrokerBusiness;
    }

    [HttpGet]
    public List<MagazineLuizaHistoryPrice> Get([FromQuery] Period period)
    {
        return _homeBrokerBusiness.GetHistoryData(period.StartDate, period.EndDate);        
    }

    [HttpGet("GetSMA")]
    public SMA GetSMA([FromQuery] Period period)
    {
        var sma = new SMA(); 
        for(int i=1;i<=50;i++)
        {
            double randomValue = new Random().NextDouble() * (1000 - 1) + 1;
            sma.Values.Add((decimal)randomValue);

        }
        return sma;
    }

    [HttpGet("GetEMA")]
    public EMA GetEMA()
    {
        var ema = new EMA();
        for (int i=1;i<=50;i++)
        {
            double randomValue = new Random().NextDouble() * (1000 - 1) + 1;
            ema.Values.Add((decimal)randomValue);

        }
        return ema;
    }
    public class Period 
    {
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now.AddYears(-1);

        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now;
    }
}
