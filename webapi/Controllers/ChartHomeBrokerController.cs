using Domain.Charts.Agreggates;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ChartHomeBrokerController : ControllerBase
{
    private readonly ILogger<ChartHomeBrokerController> _logger;
    private IHomeBrokerRepository __homeBrokerRepository;
    public ChartHomeBrokerController(ILogger<ChartHomeBrokerController> logger, IHomeBrokerRepository homeBrokerRepository)
    {
        _logger = logger;
        __homeBrokerRepository = homeBrokerRepository;
    }

    [HttpGet]
    public List<MagazineLuizaHistoryPrice> Get([FromQuery] Period period)
    {
        return __homeBrokerRepository.GetHistoryData(period.StartDate, period.EndDate).Result;        
    }
    public class Period 
    {
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now.AddYears(-1);

        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now;
    }
}
