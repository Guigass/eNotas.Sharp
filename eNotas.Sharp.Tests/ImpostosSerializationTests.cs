using eNotas.Sharp.Models;
using eNotas.Sharp.Tests.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eNotas.Sharp.Tests;

public class ImpostosSerializationTests
{
    [Fact]
    public void Deserialize_Fixture_PopulatesPisCofinsAndIbsCbs()
    {
        var json = FixtureLoader.Read("impostos-ibs.json");

        var model = JsonConvert.DeserializeObject<Impostos>(json);

        Assert.NotNull(model);
        Assert.Equal("041", model!.Icms!.SituacaoTributaria);
        Assert.Equal("01", model.Pis!.SituacaoTributaria);
        Assert.Equal(1.65m, model.Pis.PorAliquota!.Aliquota);
        Assert.Equal("01", model.Cofins!.SituacaoTributaria);
        Assert.Equal(7.6m, model.Cofins.PorAliquota!.Aliquota);
        Assert.Equal("000", model.IbsCbs!.SituacaoTributaria);
        Assert.Equal("000001", model.IbsCbs.ClassificacaoTributaria);
        Assert.Equal(0.10m, model.IbsCbs.Ibs!.Uf!.Aliquota);
        Assert.Equal(60.00m, model.IbsCbs.Ibs.Uf.PercentualReducaoAliquota);
        Assert.Equal(0.00m, model.IbsCbs.Ibs.Municipio!.Aliquota);
        Assert.Equal(0.90m, model.IbsCbs.Cbs!.Aliquota);
        Assert.Equal(60.00m, model.IbsCbs.Cbs.PercentualReducaoAliquota);
    }

    [Fact]
    public void Serialize_IncludesIbsCbsAndOmitsNullIpi()
    {
        var model = new Impostos
        {
            Pis = new Imposto
            {
                SituacaoTributaria = "01",
                PorAliquota = new PorAliquota { Aliquota = 1.65m }
            },
            IbsCbs = new IbsCbs
            {
                SituacaoTributaria = "000",
                ClassificacaoTributaria = "000001",
                Ibs = new Ibs
                {
                    Uf = new IbsUf
                    {
                        Aliquota = 0.10m,
                        PercentualDiferimento = 0.00m,
                        PercentualReducaoAliquota = 60.00m
                    },
                    Municipio = new IbsMunicipio { Aliquota = 0.00m }
                },
                Cbs = new Cbs
                {
                    Aliquota = 0.90m,
                    PercentualDiferimento = 0.00m,
                    PercentualReducaoAliquota = 60.00m
                }
            },
            Ipi = null
        };

        var obj = JObject.Parse(JsonConvert.SerializeObject(model));

        Assert.Equal("01", obj["pis"]?["situacaoTributaria"]?.Value<string>());
        Assert.Equal(1.65m, obj["pis"]?["porAliquota"]?["aliquota"]?.Value<decimal>());
        Assert.Equal("000", obj["ibsCbs"]?["situacaoTributaria"]?.Value<string>());
        Assert.Equal("000001", obj["ibsCbs"]?["classificacaoTributaria"]?.Value<string>());
        Assert.Equal(0.10m, obj["ibsCbs"]?["ibs"]?["uf"]?["aliquota"]?.Value<decimal>());
        Assert.Equal(0.90m, obj["ibsCbs"]?["cbs"]?["aliquota"]?.Value<decimal>());
        Assert.Null(obj["ipi"]);
    }

    [Fact]
    public void Serialize_SimplifiedIbsCbs_OnlyClassificacaoTributaria()
    {
        var model = new Impostos
        {
            IbsCbs = new IbsCbs
            {
                ClassificacaoTributaria = "000001"
            }
        };

        var obj = JObject.Parse(JsonConvert.SerializeObject(model));

        Assert.Equal("000001", obj["ibsCbs"]?["classificacaoTributaria"]?.Value<string>());
        Assert.Null(obj["ibsCbs"]?["situacaoTributaria"]);
        Assert.Null(obj["ibsCbs"]?["ibs"]);
        Assert.Null(obj["ibsCbs"]?["cbs"]);
        Assert.Null(obj["ibsCbs"]?["porAliquota"]);
    }
}
