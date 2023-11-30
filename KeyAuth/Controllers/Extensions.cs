namespace KeyAuth.Controllers;

public static class Extensions
{
    public static T As<T>(this object? obj) =>
        (T)Convert.ChangeType(obj ?? throw new InvalidOperationException(), typeof(T));
}