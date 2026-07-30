using eNotas.Sharp.Models;
using eNotas.Sharp.Tests.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eNotas.Sharp.Tests;

public class CartaCorrecaoSerializationTests
{
    [Fact]
    public void Deserialize_Fixture_PopulatesFields()
    {
        var json = FixtureLoader.Read("carta-correcao.json");

        var model = JsonConvert.DeserializeObject<CartaCorrecao>(json);

        Assert.NotNull(model);
        Assert.Equal("cce-teste-001", model!.Id);
        Assert.Equal("Homologacao", model.AmbienteEmissao);
        Assert.Equal(1, model.Numero);
        Assert.Equal("Correcao de informacoes adicionais", model.Correcao);
        Assert.NotNull(model.Nfe);
        Assert.Equal("00000000000000000000000000000000000000000000", model.Nfe!.ChaveAcesso);
    }

    [Fact]
    public void Deserialize_ResponseFixture_PopulatesFields()
    {
        var json = FixtureLoader.Read("correcao-response.json");

        var model = JsonConvert.DeserializeObject<CorrecaoResponse>(json);

        Assert.NotNull(model);
        Assert.Equal("Autorizada", model!.Status);
        Assert.Equal("000000000000000", model.ProtocoloAutorizacao);
        Assert.Equal("00000000000000000000000000000000000000000000", model.Nfe!.ChaveAcesso);
    }

    [Fact]
    public void Serialize_UsesCamelCaseAndOmitsNulls()
    {
        var model = new CartaCorrecao
        {
            Id = "cce-1",
            AmbienteEmissao = null,
            Correcao = "texto",
            Nfe = new Nfe { ChaveAcesso = "abc" }
        };

        var obj = JObject.Parse(JsonConvert.SerializeObject(model));

        Assert.Equal("cce-1", obj["id"]?.Value<string>());
        Assert.Equal("texto", obj["correcao"]?.Value<string>());
        Assert.Equal("abc", obj["nfe"]?["chaveAcesso"]?.Value<string>());
        Assert.Null(obj["ambienteEmissao"]);
    }
}
