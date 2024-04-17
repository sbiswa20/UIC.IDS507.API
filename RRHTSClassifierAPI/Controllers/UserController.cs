using Microsoft.AspNetCore.Mvc;

namespace RRHTSCLASSIFIERAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        Console.WriteLine(config.GetConnectionString("HTSDBConnection"));
        _dapper = new DataContextDapper(config);
    }


    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("Select GETDATE()");
    }
    

    [HttpGet("test")]
    public String[] Test()
    {
        string[] response = new string[]{
            "test1",
            "test2"
        };

        Console.WriteLine("HI ******");

        return response;
    }

}
