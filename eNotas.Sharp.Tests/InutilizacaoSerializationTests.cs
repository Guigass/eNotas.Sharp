using eNotas.Sharp.Models;
using eNotas.Sharp.Tests.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eNotas.Sharp.Tests;

public class InutilizacaoSerializationTests
{
    [Fact]
    public void Deserialize_Fixture_PopulatesFields()
    {
        var json = FixtureLoader.Read("inutilizacao.json");

        var model = JsonConvert.DeserializeObject<Inutilizacao>(json);

        Assert.NotNull(model);
        Assert.Equal("inut-teste-001", model!.Id);
        Assert.Equal("Homologacao", model.AmbienteEmissao);
        Assert.Equal("1", model.Serie);
        Assert.Equal(100, model.NumeroInicial);
        Assert.Equal(105, model.NumeroFinal);
        Assert.Equal("Numeracao nao utilizada em homologacao", model.Justificativa);
    }

    [Fact]
    public void Deserialize_ConsultaFixture_PopulatesFields()
    {
        var json = FixtureLoader.Read("consulta-inutilizacao.json");

        var model = JsonConvert.DeserializeObject<ConsultaInutilizacao>(json);

        Assert.NotNull(model);
        Assert.Equal("Autorizada", model!.Status);
        Assert.Equal(1, model.Serie);
        Assert.Equal(100, model.NumeroInicial);
    }

    [Fact]
    public void Serialize_UsesCamelCaseAndOmitsNulls()
    {
        var model = new Inutilizacao
        {
            Id = "x",
            AmbienteEmissao = null,
            Serie = "1",
            NumeroInicial = 1,
            NumeroFinal = 2,
            Justificativa = "ok"
        };

        var obj = JObject.Parse(JsonConvert.SerializeObject(model));

        Assert.Equal("x", obj["id"]?.Value<string>());
        Assert.Equal("1", obj["serie"]?.Value<string>());
        Assert.Null(obj["ambienteEmissao"]);
    }
}
