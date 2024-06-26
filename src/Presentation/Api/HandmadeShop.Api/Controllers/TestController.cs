﻿using Asp.Versioning;
using HandmadeShop.Services.Logger.Logger;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeShop.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("v{version:apiVersion}/[controller]")]
public class TestController(IAppLogger logger) : ControllerBase
{
    [HttpGet]
    [ApiVersion("1.0")]
    public int Test(int value)
    {
        logger.Debug(this, "Executed {0}, value={1}", "GET:/v1/test/", value);

        return value;
    }


    [HttpGet]
    [ApiVersion("2.0")]
    public int Test(int value, int value2)
    {
        logger.Debug(this, "Executed {0}, value={1}, value2={2}", "GET:/v2/test/", value, value2);

        return value * value2;
    }
}
