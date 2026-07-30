using eNotas.Sharp.Models;
using eNotas.Sharp.Tests.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eNotas.Sharp.Tests;

public class NotaSerializationTests
{
    [Fact]
    public void Deserialize_Fixture_PopulatesKnownFields()
    {
        var json = FixtureLoader.Read("nota-emissao-minima.json");

        var nota = JsonConvert.DeserializeObject<Nota>(json);

        Assert.NotNull(nota);
        Assert.Equal("teste-unitario-001", nota!.Id);
        Assert.Equal("NF-e", nota.Tipo);
        Assert.Equal("Homologacao", nota.AmbienteEmissao);
        Assert.False(nota.EnviarPorEmail);
        Assert.False(nota.ForcarEmissaoContingencia);
        Assert.Equal(1.00m, nota.ValorTotal);
        Assert.NotNull(nota.Cliente);
        Assert.Equal("Cliente Teste", nota.Cliente!.Nome);
        Assert.NotNull(nota.Itens);
        Assert.Single(nota.Itens!);
        Assert.Equal("5101", nota.Itens[0].Cfop);
        Assert.Equal("Produto Teste", nota.Itens[0].Descricao);
        Assert.NotNull(nota.Itens[0].Impostos?.Icms);
        Assert.Equal("041", nota.Itens[0].Impostos!.Icms!.SituacaoTributaria);
    }

    [Fact]
    public void Serialize_UsesCamelCaseAndOmitsNulls()
    {
        var nota = new Nota
        {
            Id = "abc",
            Tipo = "NF-e",
            AmbienteEmissao = "Homologacao",
            NaturezaOperacao = null,
            ValorTotal = 10.5m,
            Itens = new List<Iten>
            {
                new Iten
                {
                    Cfop = "5101",
                    Descricao = "Item",
                    Sku = null
                }
            }
        };

        var json = JsonConvert.SerializeObject(nota);
        var obj = JObject.Parse(json);

        Assert.Equal("abc", obj["id"]?.Value<string>());
        Assert.Equal("NF-e", obj["tipo"]?.Value<string>());
        Assert.Equal("Homologacao", obj["ambienteEmissao"]?.Value<string>());
        Assert.NotNull(obj["itens"]);
        Assert.Null(obj["naturezaOperacao"]);
        Assert.Null(obj["itens"]![0]!["sku"]);
        Assert.Null(obj["forcarEmissaoContingencia"]);
        Assert.Null(obj["emitidaEmContingencia"]);
    }

    [Fact]
    public void Serialize_IncludesContingenciaWhenSet()
    {
        var nota = new Nota
        {
            Id = "abc",
            ForcarEmissaoContingencia = false,
            EmitidaEmContingencia = true
        };

        var json = JsonConvert.SerializeObject(nota);
        var obj = JObject.Parse(json);

        Assert.False(obj["forcarEmissaoContingencia"]?.Value<bool>());
        Assert.True(obj["emitidaEmContingencia"]?.Value<bool>());
    }

    [Fact]
    public void RoundTrip_PreservesCoreFields()
    {
        var json = FixtureLoader.Read("nota-emissao-minima.json");
        var nota = JsonConvert.DeserializeObject<Nota>(json);
        var serialized = JsonConvert.SerializeObject(nota);
        var again = JsonConvert.DeserializeObject<Nota>(serialized);

        Assert.NotNull(again);
        Assert.Equal(nota!.Id, again!.Id);
        Assert.Equal(nota.Tipo, again.Tipo);
        Assert.Equal(nota.ValorTotal, again.ValorTotal);
        Assert.Equal(nota.Cliente!.CpfCnpj, again.Cliente!.CpfCnpj);
        Assert.Equal(nota.Itens![0].Ncm, again.Itens![0].Ncm);
    }
}
