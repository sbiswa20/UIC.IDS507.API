using Microsoft.AspNetCore.Mvc;

namespace RRHTSCLASSIFIERAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HTSCodeController : ControllerBase
{


    HTSCodeContextDapper _dapper;
    public HTSCodeController(IConfiguration config)
    {
        Console.WriteLine(config.GetConnectionString("HTSDBConnection"));
        _dapper = new HTSCodeContextDapper(config);
    }


    [HttpGet("getHTSCodes/{classificationText}")]
    public String GetHTSCodes(String classificationText)
    {
       

        return  _dapper.LoadHTSCodes(classificationText);
    }
}
