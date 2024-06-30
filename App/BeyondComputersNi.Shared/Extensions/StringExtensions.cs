namespace BeyondComputersNi.Shared.Extensions;

public static class StringExtensions
{
    public static int? ToNullableInt(this string? s)
    {
        return int.TryParse(s, out int i) ? i : null;
    }
}
