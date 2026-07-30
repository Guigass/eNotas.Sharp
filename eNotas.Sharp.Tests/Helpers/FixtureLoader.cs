namespace eNotas.Sharp.Tests.Helpers;

internal static class FixtureLoader
{
    public static string Read(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Fixtures", fileName);
        return File.ReadAllText(path);
    }
}
