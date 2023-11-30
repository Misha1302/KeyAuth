namespace KeyAuth.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class KeyAuthController : ControllerBase
{
    private readonly AppContext _appContext;
    private readonly IConfiguration _configuration;

    public KeyAuthController(AppContext appContext, IConfiguration configuration)
    {
        _appContext = appContext;
        _configuration = configuration;
    }

    [HttpPost("Registrations")]
    public IResult TryActivate([FromBody] string key)
    {
        var info = _appContext.Keys.FirstOrDefault(x => x.Key == key);

        if (info is null)
            return Results.NotFound($"Unknown key ({key})");

        if (info.ActivatedCount >= _configuration["MaxActivateCount"].As<int>())
            return Results.Conflict($"The key has already been activated {info.ActivatedCount} times");

        info.ActivatedCount++;
        _appContext.SaveChanges();
        return Results.Ok();
    }

    [HttpPost("Keys")]
    public IResult AddKey(KeyAddInfo keyInfo)
    {
        if (keyInfo.Password != _configuration["Password"].As<string>())
            return Results.Problem("Invalid password");

        if (_appContext.Keys.Any(x => x.Key == keyInfo.Key))
            return Results.Conflict("This key has already been added");

        var entity = new KeyInfo
        {
            Key = keyInfo.Key,
            FullName = keyInfo.FullName,
            ActivatedCount = 0
        };
        _appContext.Keys.Add(entity);

        _appContext.SaveChanges();

        return Results.Ok();
    }
}