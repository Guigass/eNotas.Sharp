using eNotas.Sharp.Models;
using eNotas.Sharp.Tests.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eNotas.Sharp.Tests;

public class EmpresaSerializationTests
{
    [Fact]
    public void Deserialize_Fixture_PopulatesKnownFields()
    {
        var json = FixtureLoader.Read("empresa-incluir-alterar.json");

        var empresa = JsonConvert.DeserializeObject<Empresa>(json);

        Assert.NotNull(empresa);
        Assert.Equal("99999999999999", empresa!.Cnpj);
        Assert.Equal("999999", empresa.InscricaoMunicipal);
        Assert.Equal("Cliente Teste", empresa.RazaoSocial);
        Assert.Equal("Cliente Teste", empresa.NomeFantasia);
        Assert.True(empresa.OptanteSimplesNacional);
        Assert.Equal("email@email.com.br", empresa.Email);
        Assert.False(empresa.IncentivadorCultural);
        Assert.Equal("0", empresa.RegimeEspecialTributacao);
        Assert.Equal("4219", empresa.CodigoServicoMunicipal);
        Assert.Equal(0.02m, empresa.AliquotaIss);
        Assert.Equal("Discriminacao do servico prestado.", empresa.DescricaoServico);
        Assert.True(empresa.EnviarEmailCliente);

        Assert.NotNull(empresa.Endereco);
        Assert.Equal(31, empresa.Endereco!.CodigoIbgeUf);
        Assert.Equal(3106200, empresa.Endereco.CodigoIbgeCidade);
        Assert.Equal("MG", empresa.Endereco.Uf);
        Assert.Equal("Belo Horizonte", empresa.Endereco.Cidade);
        Assert.Equal("85100000", empresa.Endereco.Cep);

        Assert.NotNull(empresa.ConfiguracoesNfseHomologacao);
        Assert.Equal(1, empresa.ConfiguracoesNfseHomologacao!.SequencialNFe);
        Assert.Equal("NF", empresa.ConfiguracoesNfseHomologacao.SerieNFe);
        Assert.Equal(1, empresa.ConfiguracoesNfseHomologacao.SequencialLoteNFe);

        Assert.NotNull(empresa.ConfiguracoesNfseProducao);
        Assert.Equal(1, empresa.ConfiguracoesNfseProducao!.SequencialNFe);
        Assert.Equal("NF", empresa.ConfiguracoesNfseProducao.SerieNFe);
    }

    [Fact]
    public void Serialize_UsesCamelCaseAndOmitsNulls()
    {
        var empresa = new Empresa
        {
            Cnpj = "99999999999999",
            RazaoSocial = "Cliente Teste",
            InscricaoEstadual = null,
            TelefoneComercial = null,
            Endereco = new Endereco
            {
                CodigoIbgeUf = 31,
                CodigoIbgeCidade = 3106200,
                Uf = "MG",
                Cidade = "Belo Horizonte"
            },
            ConfiguracoesNfseHomologacao = new ConfiguracoesNfse
            {
                SequencialNFe = 1,
                SerieNFe = "NF",
                SequencialLoteNFe = 1,
                UsuarioAcessoProvedor = null
            }
        };

        var json = JsonConvert.SerializeObject(empresa);
        var obj = JObject.Parse(json);

        Assert.Equal("99999999999999", obj["cnpj"]?.Value<string>());
        Assert.Equal("Cliente Teste", obj["razaoSocial"]?.Value<string>());
        Assert.Null(obj["inscricaoEstadual"]);
        Assert.Null(obj["telefoneComercial"]);
        Assert.Equal(31, obj["endereco"]?["codigoIbgeUf"]?.Value<int>());
        Assert.Equal(3106200, obj["endereco"]?["codigoIbgeCidade"]?.Value<int>());
        Assert.NotNull(obj["configuracoesNFSeHomologacao"]);
        Assert.Null(obj["configuracoesNFSeProducao"]);
        Assert.Equal(1, obj["configuracoesNFSeHomologacao"]?["sequencialNFe"]?.Value<int>());
        Assert.Null(obj["configuracoesNFSeHomologacao"]?["usuarioAcessoProvedor"]);
    }

    [Fact]
    public void Deserialize_PascalCaseConfigKeys_PopulatesConfigs()
    {
        const string json = """
            {
              "cnpj": "99999999999999",
              "ConfiguracoesNFSeHomologacao": {
                "sequencialNFe": 2,
                "serieNFe": "NF"
              },
              "ConfiguracoesNFSeProducao": {
                "sequencialNFe": 3,
                "serieNFe": "NF"
              }
            }
            """;

        var empresa = JsonConvert.DeserializeObject<Empresa>(json);

        Assert.NotNull(empresa);
        Assert.Equal(2, empresa!.ConfiguracoesNfseHomologacao!.SequencialNFe);
        Assert.Equal(3, empresa.ConfiguracoesNfseProducao!.SequencialNFe);
    }
}
