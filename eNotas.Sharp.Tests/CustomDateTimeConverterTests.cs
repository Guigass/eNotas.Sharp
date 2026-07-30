using eNotas.Sharp.Models;
using Newtonsoft.Json;

namespace eNotas.Sharp.Tests;

public class CustomDateTimeConverterTests
{
    [Fact]
    public void WriteJson_UsesUtcRoundTripFormat()
    {
        var nota = new Nota
        {
            DataEmissao = new DateTimeOffset(2024, 6, 15, 12, 30, 0, TimeSpan.Zero)
        };

        var json = JsonConvert.SerializeObject(nota);

        Assert.Contains("\"dataEmissao\":\"2024-06-15T12:30:00.0000000Z\"", json);
    }

    [Fact]
    public void ReadJson_DoesNotThrow()
    {
        const string json = "{\"dataEmissao\":\"2024-06-15T12:30:00Z\"}";

        var nota = JsonConvert.DeserializeObject<Nota>(json);

        Assert.NotNull(nota);
        Assert.NotNull(nota!.DataEmissao);
        Assert.Equal(2024, nota.DataEmissao!.Value.Year);
        Assert.Equal(6, nota.DataEmissao.Value.Month);
        Assert.Equal(15, nota.DataEmissao.Value.Day);
    }
}
