using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplication1.Controllers;
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]  
    public class WeatherForecastController(IConfiguration configuration, IOptions<MyInfoConfig> myInfoConfig) : ControllerBase
    {
        [HttpGet]
        public int GetTotalPresentStudent()
        {
            int count = configuration.GetValue<int>("TotalPresentStudent"); //getvalue key(totalpresentstudent) ko basis ma configuration ma jati pani key value pair hunxa tyo get garna milxa
            return count;
        }
        [HttpGet("myinfo")]
        public object GetMyInformation()
        {
            var data = new
            {
                Name = configuration["MyInfo:Name"],
                Age = configuration["MyInfo:Age"],
                Address = configuration["MyInfo:Address"]
            };
            return data;
        }
        [HttpGet("myinfo-option-pattern")]
        public object GetMyInformationOptionPattern()
        {
            var myInfoConfigValue = myInfoConfig.Value;
            var data = new
            {
                Name = myInfoConfigValue.Name,
                Age = myInfoConfigValue.Age,
                Address = myInfoConfigValue.Address
            };
            return data;
        }
    }
}