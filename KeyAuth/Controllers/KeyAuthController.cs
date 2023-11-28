namespace KeyAuth.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class KeyAuthController : ControllerBase
{
    private readonly AppContext _appContext;

    public KeyAuthController(AppContext appContext)
    {
        _appContext = appContext;
    }

    [HttpGet]
    public IResult TryActivate(string key)
    {
        var info = _appContext.Keys.FirstOrDefault(x => x.Key == key);

        if (info is null)
            return Results.NotFound($"Unknown key ({key})");

        if (info.ActivatedCount >= Constants.MaxActiveCount)
            return Results.Conflict($"The key has already been activated {info.ActivatedCount} times");

        info.ActivatedCount++;
        _appContext.SaveChanges();
        return Results.Ok();
    }

    [HttpPost]
    public IResult AddKey(KeyAddInfo keyInfo, string password)
    {
        if (password != Constants.Password)
            return Results.Problem("Invalid password");

        if (_appContext.Keys.Any(x => x.Key == keyInfo.Key))
            return Results.Conflict("This key has already been added");

        _appContext.Keys.Add(new KeyInfo
        {
            Key = keyInfo.Key,
            FullName = keyInfo.FullName,
            ActivatedCount = 0
        });

        _appContext.SaveChanges();

        return Results.Ok();
    }
}