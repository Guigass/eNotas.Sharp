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
        AssertRoundTripPreservesJsonShape("nfse-emissao.json");
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
        AssertRoundTripPreservesJsonShape("nfse-reforma.json");
    }

    private static void AssertRoundTripPreservesJsonShape(string fixtureName)
    {
        var json = FixtureLoader.Read(fixtureName);
        var expected = NormalizeForShapeComparison(JToken.Parse(json));

        var nfse = JsonConvert.DeserializeObject<Nfse>(json);
        Assert.NotNull(nfse);

        var actual = NormalizeForShapeComparison(JToken.Parse(JsonConvert.SerializeObject(nfse)));
        Assert.True(
            JToken.DeepEquals(expected, actual),
            $"Round-trip alterou o shape JSON de {fixtureName}.{Environment.NewLine}Esperado:{Environment.NewLine}{expected}{Environment.NewLine}Atual:{Environment.NewLine}{actual}");
    }

    // NullValueHandling.Ignore omite nulls; 1 vs 1.0 não é diferença de shape.
    private static JToken NormalizeForShapeComparison(JToken token)
    {
        StripNullProperties(token);
        NormalizeNumbers(token);
        return token;
    }

    private static void StripNullProperties(JToken token)
    {
        if (token is JObject obj)
        {
            foreach (var prop in obj.Properties().ToList())
            {
                if (prop.Value.Type == JTokenType.Null)
                    prop.Remove();
                else
                    StripNullProperties(prop.Value);
            }
        }
        else if (token is JArray arr)
        {
            foreach (var item in arr)
                StripNullProperties(item);
        }
    }

    private static void NormalizeNumbers(JToken token)
    {
        if (token is JObject obj)
        {
            foreach (var prop in obj.Properties())
                NormalizeNumbers(prop.Value);
        }
        else if (token is JArray arr)
        {
            foreach (var item in arr)
                NormalizeNumbers(item);
        }
        else if (token is JValue { Type: JTokenType.Integer or JTokenType.Float } value)
        {
            value.Replace(new JValue(Convert.ToDecimal(value.Value)));
        }
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

    [Fact]
    public void Serialize_NfseNewFields_UsesCamelCaseAndOmitsNulls()
    {
        var nfse = new Nfse
        {
            Tipo = "NFS-e",
            IdExterno = "NFSE-CAMPOS-SER-001",
            AmbienteEmissao = "Producao",
            EnviarPorEmail = true,
            DataCompetencia = DateTimeOffset.Parse("2024-10-23T12:00:00Z"),
            NaturezaOperacao = "Tributacao no municipio",
            Observacoes = "Observacao de teste",
            DadosAdicionaisEmail = new DadosAdicionaisEmail
            {
                OutrosDestinatarios = "outro1@teste.com.br;outro2@teste.com.br"
            },
            Deducoes = 10.50m,
            Descontos = 5.25m,
            DescontoCondicionado = 2.00m,
            ValorTotal = 1000.00m
        };

        var json = JsonConvert.SerializeObject(nfse);
        var obj = JObject.Parse(json);

        Assert.Contains("\"dataCompetencia\":\"2024-10-23T12:00:00.0000000Z\"", json);
        Assert.Equal("Tributacao no municipio", obj["naturezaOperacao"]?.Value<string>());
        Assert.Equal("Observacao de teste", obj["observacoes"]?.Value<string>());
        Assert.Equal("outro1@teste.com.br;outro2@teste.com.br", obj["dadosAdicionaisEmail"]?["outrosDestinatarios"]?.Value<string>());
        Assert.Equal(10.50m, obj["deducoes"]?.Value<decimal>());
        Assert.Equal(5.25m, obj["descontos"]?.Value<decimal>());
        Assert.Equal(2.00m, obj["descontoCondicionado"]?.Value<decimal>());
        Assert.Null(obj["cliente"]);
        Assert.Null(obj["servico"]);
    }

    [Fact]
    public void Deserialize_CamposEnotasFixture_PopulatesNewNfseAndServicoFields()
    {
        var json = FixtureLoader.Read("nfse-emissao-campos-enotas.json");

        var nfse = JsonConvert.DeserializeObject<Nfse>(json);

        Assert.NotNull(nfse);
        Assert.Equal("NFSE-CAMPOS-001", nfse!.IdExterno);
        Assert.Equal(DateTimeOffset.Parse("2024-10-23T12:00:00Z"), nfse.DataCompetencia);
        Assert.Equal("Tributacao no municipio", nfse.NaturezaOperacao);
        Assert.Equal("Nota de teste com campos adicionais", nfse.Observacoes);
        Assert.NotNull(nfse.DadosAdicionaisEmail);
        Assert.Equal("outro1@teste.com.br;outro2@teste.com.br", nfse.DadosAdicionaisEmail!.OutrosDestinatarios);
        Assert.Equal(10.50m, nfse.Deducoes);
        Assert.Equal(5.25m, nfse.Descontos);
        Assert.Equal(2.00m, nfse.DescontoCondicionado);

        Assert.NotNull(nfse.Servico);
        Assert.False(nfse.Servico!.Exportacao);
        Assert.Equal("Nenhum", nfse.Servico.RegimeEspecialTributacao);
        Assert.Equal("Nenhuma", nfse.Servico.TipoImunidadeIss);
        Assert.Equal("Brasil", nfse.Servico.PaisPrestacaoServico);
        Assert.Equal("MG", nfse.Servico.UfPrestacaoServico);

        Assert.NotNull(nfse.Servico.ExigibilidadeSuspensa);
        Assert.Equal("Decisao judicial", nfse.Servico.ExigibilidadeSuspensa!.Tipo);
        Assert.Equal("123456789", nfse.Servico.ExigibilidadeSuspensa.NumeroProcesso);

        Assert.NotNull(nfse.Servico.PisCofinsApuracaoPropria);
        Assert.Equal(1000.00m, nfse.Servico.PisCofinsApuracaoPropria!.BaseCalculo);
        Assert.Equal(0.65m, nfse.Servico.PisCofinsApuracaoPropria.AliquotaPis);
        Assert.Equal(6.50m, nfse.Servico.PisCofinsApuracaoPropria.ValorPis);
        Assert.Equal(3.00m, nfse.Servico.PisCofinsApuracaoPropria.AliquotaCofins);
        Assert.Equal(30.00m, nfse.Servico.PisCofinsApuracaoPropria.ValorCofins);

        Assert.Equal("01", nfse.Servico.SituacaoTributariaPisCofins);
        Assert.Equal("NaoRetido", nfse.Servico.TipoRetencaoPisCofins);
        Assert.Equal(6.50m, nfse.Servico.ValorPis);
        Assert.Equal(30.00m, nfse.Servico.ValorCofins);
        Assert.Equal(10.00m, nfse.Servico.ValorCsll);
        Assert.Equal(20.00m, nfse.Servico.ValorInss);
        Assert.Equal(15.00m, nfse.Servico.ValorIr);
    }

    [Fact]
    public void Serialize_ServicoNewFields_UsesCamelCaseAndOmitsNulls()
    {
        var nfse = new Nfse
        {
            Tipo = "NFS-e",
            IdExterno = "NFSE-SERVICO-SER-001",
            Servico = new Servico
            {
                Descricao = "Servico",
                Exportacao = true,
                RegimeEspecialTributacao = "Nenhum",
                TipoImunidadeIss = "Nenhuma",
                PaisPrestacaoServico = "Brasil",
                UfPrestacaoServico = "SP",
                ExigibilidadeSuspensa = new ExigibilidadeSuspensa
                {
                    Tipo = "Decisao judicial",
                    NumeroProcesso = "123"
                },
                PisCofinsApuracaoPropria = new PisCofinsApuracaoPropria
                {
                    BaseCalculo = 100.00m,
                    AliquotaPis = 0.65m,
                    ValorPis = 0.65m,
                    AliquotaCofins = 3.00m,
                    ValorCofins = 3.00m
                },
                SituacaoTributariaPisCofins = "01",
                TipoRetencaoPisCofins = "NaoRetido",
                ValorPis = 0.65m,
                ValorCofins = 3.00m,
                ValorCsll = 1.00m,
                ValorInss = 2.00m,
                ValorIr = 1.50m
            }
        };

        var json = JsonConvert.SerializeObject(nfse);
        var obj = JObject.Parse(json);
        var servico = obj["servico"];

        Assert.True(servico?["exportacao"]?.Value<bool>());
        Assert.Equal("Nenhum", servico?["regimeEspecialTributacao"]?.Value<string>());
        Assert.Equal("Nenhuma", servico?["tipoImunidadeIss"]?.Value<string>());
        Assert.Equal("Brasil", servico?["paisPrestacaoServico"]?.Value<string>());
        Assert.Equal("SP", servico?["ufPrestacaoServico"]?.Value<string>());
        Assert.Equal("Decisao judicial", servico?["exigibilidadeSuspensa"]?["tipo"]?.Value<string>());
        Assert.Equal("123", servico?["exigibilidadeSuspensa"]?["numeroProcesso"]?.Value<string>());
        Assert.Equal(100.00m, servico?["pisCofinsApuracaoPropria"]?["baseCalculo"]?.Value<decimal>());
        Assert.Equal(0.65m, servico?["pisCofinsApuracaoPropria"]?["aliquotaPis"]?.Value<decimal>());
        Assert.Equal(0.65m, servico?["pisCofinsApuracaoPropria"]?["valorPis"]?.Value<decimal>());
        Assert.Equal(3.00m, servico?["pisCofinsApuracaoPropria"]?["aliquotaCofins"]?.Value<decimal>());
        Assert.Equal(3.00m, servico?["pisCofinsApuracaoPropria"]?["valorCofins"]?.Value<decimal>());
        Assert.Equal("01", servico?["situacaoTributariaPisCofins"]?.Value<string>());
        Assert.Equal("NaoRetido", servico?["tipoRetencaoPisCofins"]?.Value<string>());
        Assert.Equal(0.65m, servico?["valorPis"]?.Value<decimal>());
        Assert.Equal(3.00m, servico?["valorCofins"]?.Value<decimal>());
        Assert.Equal(1.00m, servico?["valorCsll"]?.Value<decimal>());
        Assert.Equal(2.00m, servico?["valorInss"]?.Value<decimal>());
        Assert.Equal(1.50m, servico?["valorIr"]?.Value<decimal>());
    }

    [Fact]
    public void RoundTrip_CamposEnotasFixture_PreservesJsonShape()
    {
        AssertRoundTripPreservesJsonShape("nfse-emissao-campos-enotas.json");
    }
}
