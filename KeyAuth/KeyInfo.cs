namespace KeyAuth;

using System.ComponentModel.DataAnnotations;

// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
public class KeyInfo
{
    [Key]
    public int Id { get; set; }

    public string Key { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public int ActivatedCount { get; set; }
}

public record KeyAddInfo(string Key, string FullName, string Password);