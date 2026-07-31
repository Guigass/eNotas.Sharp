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
        Assert.Equal("00", nota.Itens[0].Extipi);
        Assert.Equal("SP123456", nota.Itens[0].CodigoBeneficioFiscal);
        Assert.Equal(1.0m, nota.Itens[0].QuantidadeTributavel);
        Assert.Equal("un", nota.Itens[0].UnidadeMedidaTributavel);
        Assert.Equal(1.00m, nota.Itens[0].ValorTotal);
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
        Assert.Null(obj["indicadorPresencaConsumidor"]);
        Assert.Null(obj["itens"]![0]!["extipi"]);
        Assert.Null(obj["itens"]![0]!["codigoBeneficioFiscal"]);
        Assert.Null(obj["itens"]![0]!["quantidadeTributavel"]);
        Assert.Null(obj["itens"]![0]!["unidadeMedidaTributavel"]);
        Assert.Null(obj["itens"]![0]!["valorTotal"]);
    }

    [Fact]
    public void Serialize_IncludesItenOptionalFieldsWhenSet()
    {
        var nota = new Nota
        {
            Id = "abc",
            Itens = new List<Iten>
            {
                new Iten
                {
                    Cfop = "5101",
                    Extipi = "00",
                    CodigoBeneficioFiscal = "SP123456",
                    QuantidadeTributavel = 2.5m,
                    UnidadeMedidaTributavel = "kg",
                    ValorTotal = 10.0m
                }
            }
        };

        var json = JsonConvert.SerializeObject(nota);
        var item = JObject.Parse(json)["itens"]![0]!;

        Assert.Equal("00", item["extipi"]?.Value<string>());
        Assert.Equal("SP123456", item["codigoBeneficioFiscal"]?.Value<string>());
        Assert.Equal(2.5m, item["quantidadeTributavel"]?.Value<decimal>());
        Assert.Equal("kg", item["unidadeMedidaTributavel"]?.Value<string>());
        Assert.Equal(10.0m, item["valorTotal"]?.Value<decimal>());
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
    public void Serialize_IncludesIndicadorPresencaConsumidorWhenSet()
    {
        var nota = new Nota
        {
            Id = "abc",
            IndicadorPresencaConsumidor = "NaoSeAplica"
        };

        var json = JsonConvert.SerializeObject(nota);
        var obj = JObject.Parse(json);

        Assert.Equal("NaoSeAplica", obj["indicadorPresencaConsumidor"]?.Value<string>());
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
        Assert.Equal(nota.Itens[0].Extipi, again.Itens[0].Extipi);
        Assert.Equal(nota.Itens[0].CodigoBeneficioFiscal, again.Itens[0].CodigoBeneficioFiscal);
        Assert.Equal(nota.Itens[0].QuantidadeTributavel, again.Itens[0].QuantidadeTributavel);
        Assert.Equal(nota.Itens[0].UnidadeMedidaTributavel, again.Itens[0].UnidadeMedidaTributavel);
        Assert.Equal(nota.Itens[0].ValorTotal, again.Itens[0].ValorTotal);
    }
}
