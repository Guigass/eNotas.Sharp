using eNotas.Sharp.Models;
using eNotas.Sharp.Tests.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eNotas.Sharp.Tests;

public class NfseSerializationTests
{
    [Fact]
    public void Deserialize_Fixture_PopulatesKnownFields()
    {
        var json = FixtureLoader.Read("nfse-emissao.json");

        var nfse = JsonConvert.DeserializeObject<Nfse>(json);

        Assert.NotNull(nfse);
        Assert.Equal("NFS-e", nfse!.Tipo);
        Assert.Equal("TESTE231024", nfse.IdExterno);
        Assert.Equal("Producao", nfse.AmbienteEmissao);
        Assert.False(nfse.EnviarPorEmail);
        Assert.Equal(1m, nfse.ValorTotal);

        Assert.NotNull(nfse.Cliente);
        Assert.Equal("F", nfse.Cliente!.TipoPessoa);
        Assert.Equal("Cliente teste", nfse.Cliente.Nome);
        Assert.Equal("teste@teste.com.br", nfse.Cliente.Email);
        Assert.Equal("38618699772", nfse.Cliente.CpfCnpj);
        Assert.Equal("3132223333", nfse.Cliente.Telefone);

        Assert.NotNull(nfse.Cliente.Endereco);
        Assert.Equal("Brasil", nfse.Cliente.Endereco!.Pais);
        Assert.Equal("MG", nfse.Cliente.Endereco.Uf);
        Assert.Equal("Belo Horizonte", nfse.Cliente.Endereco.Cidade);
        Assert.Equal("Rua Sergipe", nfse.Cliente.Endereco.Logradouro);
        Assert.Equal("1014", nfse.Cliente.Endereco.Numero);
        Assert.Equal("7o Andar", nfse.Cliente.Endereco.Complemento);
        Assert.Equal("Savassi", nfse.Cliente.Endereco.Bairro);
        Assert.Equal("30130174", nfse.Cliente.Endereco.Cep);

        Assert.NotNull(nfse.Servico);
        Assert.Equal("Teste webservice", nfse.Servico!.Descricao);
        Assert.Equal(3.0m, nfse.Servico.AliquotaIss);
        Assert.False(nfse.Servico.IssRetidoFonte);
        Assert.Equal("4.12", nfse.Servico.CodigoServicoMunicipio);
        Assert.Equal("4.12", nfse.Servico.ItemListaServicoLC116);
        Assert.Equal("8630504", nfse.Servico.Cnae);
        Assert.Equal("3300704", nfse.Servico.MunicipioPrestacaoServico);
    }

    [Fact]
    public void Serialize_UsesCamelCaseAndOmitsNulls()
    {
        var nfse = new Nfse
        {
            Tipo = "NFS-e",
            IdExterno = "TESTE231024",
            AmbienteEmissao = "Producao",
            EnviarPorEmail = false,
            ValorTotal = 1m,
            Cliente = new Cliente
            {
                TipoPessoa = "F",
                Nome = "Cliente teste",
                Email = "teste@teste.com.br",
                CpfCnpj = "38618699772",
                InscricaoMunicipal = null,
                InscricaoEstadual = null,
                Telefone = "3132223333",
                Endereco = new Endereco
                {
                    Pais = "Brasil",
                    Uf = "MG",
                    Cidade = "Belo Horizonte",
                    Logradouro = "Rua Sergipe",
                    Numero = "1014",
                    Complemento = "7o Andar",
                    Bairro = "Savassi",
                    Cep = "30130174"
                }
            },
            Servico = new Servico
            {
                Descricao = "Teste webservice",
                AliquotaIss = 3.0m,
                IssRetidoFonte = false,
                CodigoServicoMunicipio = "4.12",
                ItemListaServicoLC116 = "4.12",
                Cnae = "8630504",
                MunicipioPrestacaoServico = "3300704"
            }
        };

        var json = JsonConvert.SerializeObject(nfse);
        var obj = JObject.Parse(json);

        Assert.Equal("NFS-e", obj["tipo"]?.Value<string>());
        Assert.Equal("TESTE231024", obj["idExterno"]?.Value<string>());
        Assert.Equal("Producao", obj["ambienteEmissao"]?.Value<string>());
        Assert.False(obj["enviarPorEmail"]?.Value<bool>());
        Assert.Null(obj["enviadaPorEmail"]);
        Assert.Equal(1m, obj["valorTotal"]?.Value<decimal>());
        Assert.Null(obj["cliente"]?["inscricaoMunicipal"]);
        Assert.Null(obj["cliente"]?["inscricaoEstadual"]);
        Assert.Equal("Cliente teste", obj["cliente"]?["nome"]?.Value<string>());
        Assert.Equal("MG", obj["cliente"]?["endereco"]?["uf"]?.Value<string>());
        Assert.Equal("Teste webservice", obj["servico"]?["descricao"]?.Value<string>());
        Assert.Equal(3.0m, obj["servico"]?["aliquotaIss"]?.Value<decimal>());
        Assert.False(obj["servico"]?["issRetidoFonte"]?.Value<bool>());
        Assert.Equal("4.12", obj["servico"]?["codigoServicoMunicipio"]?.Value<string>());
        Assert.Equal("4.12", obj["servico"]?["itemListaServicoLC116"]?.Value<string>());
        Assert.Equal("8630504", obj["servico"]?["cnae"]?.Value<string>());
        Assert.Equal("3300704", obj["servico"]?["municipioPrestacaoServico"]?.Value<string>());
        Assert.Null(obj["codigoNBS"]);
        Assert.Null(obj["servico"]?["codigoNBS"]);
        Assert.Null(obj["servico"]?["ibsCbs"]);
    }

    [Fact]
    public void RoundTrip_Fixture_PreservesJsonShape()
    {
        var json = FixtureLoader.Read("nfse-emissao.json");
        var nfse = JsonConvert.DeserializeObject<Nfse>(json);
        Assert.NotNull(nfse);

        var serialized = JsonConvert.SerializeObject(nfse);
        var again = JsonConvert.DeserializeObject<Nfse>(serialized);

        Assert.NotNull(again);
        Assert.Equal(nfse!.Tipo, again!.Tipo);
        Assert.Equal(nfse.IdExterno, again.IdExterno);
        Assert.Equal(nfse.AmbienteEmissao, again.AmbienteEmissao);
        Assert.Equal(nfse.EnviarPorEmail, again.EnviarPorEmail);
        Assert.Equal(nfse.ValorTotal, again.ValorTotal);
        Assert.Equal(nfse.Cliente!.Nome, again.Cliente!.Nome);
        Assert.Equal(nfse.Cliente.Endereco!.Cidade, again.Cliente.Endereco!.Cidade);
        Assert.Equal(nfse.Servico!.CodigoServicoMunicipio, again.Servico!.CodigoServicoMunicipio);
        Assert.Equal(nfse.Servico.MunicipioPrestacaoServico, again.Servico.MunicipioPrestacaoServico);
    }

#pragma warning disable CS0618 // EnviadaPorEmail obsolete alias (compat NuGet)
    [Fact]
    public void Serialize_ObsoleteEnviadaPorEmailAlias_EmitsEnviarPorEmail()
    {
        var nfse = new Nfse
        {
            Tipo = "NFS-e",
            IdExterno = "ALIAS-001",
            AmbienteEmissao = "Homologacao",
            EnviadaPorEmail = true
        };

        var json = JsonConvert.SerializeObject(nfse);
        var obj = JObject.Parse(json);

        Assert.True(obj["enviarPorEmail"]?.Value<bool>());
        Assert.Null(obj["enviadaPorEmail"]);
        Assert.True(nfse.EnviarPorEmail);
    }
#pragma warning restore CS0618

    [Fact]
    public void Deserialize_ReformaFixture_PopulatesServicoReformaFields()
    {
        var json = FixtureLoader.Read("nfse-reforma.json");

        var nfse = JsonConvert.DeserializeObject<Nfse>(json);

        Assert.NotNull(nfse);
        Assert.NotNull(nfse!.Servico);
        Assert.Equal("101011100", nfse.Servico!.CodigoNBS);
        Assert.Equal("010101", nfse.Servico.CodigoTributacaoNacional);
        Assert.NotNull(nfse.Servico.IbsCbs);
        Assert.Equal("000001", nfse.Servico.IbsCbs!.ClassificacaoTributaria);
        Assert.Equal("030101", nfse.Servico.IbsCbs.CodigoIndicadorOperacao);
    }

    [Fact]
    public void Serialize_ReformaFields_UsesCamelCaseAndOmitsNulls()
    {
        var nfse = new Nfse
        {
            Tipo = "NFS-e",
            IdExterno = "NFSe-REFORMA-001",
            AmbienteEmissao = "Homologacao",
            ValorTotal = 1m,
            Servico = new Servico
            {
                Descricao = "Servico de consultoria",
                AliquotaIss = 3m,
                IssRetidoFonte = false,
                CodigoServicoMunicipio = "010700188",
                ItemListaServicoLC116 = "1.07",
                MunicipioPrestacaoServico = "3106200",
                CodigoNBS = "101011100",
                CodigoTributacaoNacional = "010101",
                IbsCbs = new ServicoIbsCbs
                {
                    ClassificacaoTributaria = "000001",
                    CodigoIndicadorOperacao = "030101"
                }
            }
        };

        var json = JsonConvert.SerializeObject(nfse);
        var obj = JObject.Parse(json);

        Assert.Equal("101011100", obj["servico"]?["codigoNBS"]?.Value<string>());
        Assert.Equal("010101", obj["servico"]?["codigoTributacaoNacional"]?.Value<string>());
        Assert.Equal("000001", obj["servico"]?["ibsCbs"]?["classificacaoTributaria"]?.Value<string>());
        Assert.Equal("030101", obj["servico"]?["ibsCbs"]?["codigoIndicadorOperacao"]?.Value<string>());
        Assert.Null(obj["servico"]?["ibsCbs"]?["situacaoTributaria"]);
        Assert.Null(obj["servico"]?["ibsCbs"]?["ibs"]);
        Assert.Null(obj["servico"]?["ibsCbs"]?["cbs"]);
        Assert.Null(obj["servico"]?["cnae"]);
    }

    [Fact]
    public void RoundTrip_ReformaFixture_PreservesJsonShape()
    {
        var json = FixtureLoader.Read("nfse-reforma.json");
        var nfse = JsonConvert.DeserializeObject<Nfse>(json);
        Assert.NotNull(nfse);

        var serialized = JsonConvert.SerializeObject(nfse);
        var again = JsonConvert.DeserializeObject<Nfse>(serialized);

        Assert.NotNull(again);
        Assert.Equal(nfse!.Servico!.CodigoNBS, again!.Servico!.CodigoNBS);
        Assert.Equal(nfse.Servico.CodigoTributacaoNacional, again.Servico.CodigoTributacaoNacional);
        Assert.Equal(nfse.Servico.IbsCbs!.ClassificacaoTributaria, again.Servico.IbsCbs!.ClassificacaoTributaria);
        Assert.Equal(nfse.Servico.IbsCbs.CodigoIndicadorOperacao, again.Servico.IbsCbs.CodigoIndicadorOperacao);
    }

    [Fact]
    public void Serialize_RpsFields_UsesCamelCaseAndOmitsNulls()
    {
        var withRps = new Nfse
        {
            Tipo = "NFS-e",
            IdExterno = "RPS-001",
            NumeroRps = 123L,
            SerieRps = "1"
        };

        var withRpsJson = JsonConvert.SerializeObject(withRps);
        var withRpsObj = JObject.Parse(withRpsJson);

        Assert.Equal(123L, withRpsObj["numeroRps"]?.Value<long>());
        Assert.Equal("1", withRpsObj["serieRps"]?.Value<string>());

        var withoutRps = new Nfse
        {
            Tipo = "NFS-e",
            IdExterno = "RPS-002"
        };

        var withoutRpsJson = JsonConvert.SerializeObject(withoutRps);
        var withoutRpsObj = JObject.Parse(withoutRpsJson);

        Assert.Null(withoutRpsObj["numeroRps"]);
        Assert.Null(withoutRpsObj["serieRps"]);
    }

    [Fact]
    public void Deserialize_RpsFields_PopulatesNumeroAndSerie()
    {
        const string json = """
            {
              "tipo": "NFS-e",
              "idExterno": "RPS-003",
              "numeroRps": 99,
              "serieRps": "A"
            }
            """;

        var nfse = JsonConvert.DeserializeObject<Nfse>(json);

        Assert.NotNull(nfse);
        Assert.Equal(99L, nfse!.NumeroRps);
        Assert.Equal("A", nfse.SerieRps);
    }
}
