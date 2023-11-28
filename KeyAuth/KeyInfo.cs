namespace KeyAuth;

using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Key))]
public class KeyInfo
{
    public string Key { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public int ActivatedCount { get; set; } = default!;
}

[PrimaryKey(nameof(Key))]
public class KeyAddInfo
{
    public string Key { get; set; } = default!;
    public string FullName { get; set; } = default!;
}