using eNotas.Sharp.Models;
using eNotas.Sharp.Tests.Helpers;
using Newtonsoft.Json;

namespace eNotas.Sharp.Tests;

public class ConsultaNfseSerializationTests
{
    [Fact]
    public void Deserialize_Fixture_PopulatesEvidencedFields()
    {
        var json = FixtureLoader.Read("consulta-nfse.json");

        var consulta = JsonConvert.DeserializeObject<ConsultaNfse>(json);

        Assert.NotNull(consulta);
        Assert.Equal("teste-unitario-nfse-001", consulta!.Id);
        Assert.Equal("NFS-e", consulta.Tipo);
        Assert.Equal("TESTE231024", consulta.IdExterno);
        Assert.Equal("Autorizada", consulta.Status);
        Assert.Null(consulta.MotivoStatus);
        Assert.Equal("Homologacao", consulta.AmbienteEmissao);
        Assert.False(consulta.EnviadaPorEmail);
        Assert.Equal("100", consulta.Numero);
        Assert.Equal("ABC123", consulta.CodigoVerificacao);
        Assert.Equal("00000000000000000000000000000000000000000000", consulta.ChaveAcesso);
        Assert.Equal("https://example.test/nfse.pdf", consulta.LinkDownloadPdf);
        Assert.Equal("https://example.test/nfse.xml", consulta.LinkDownloadXml);
        Assert.Equal(1.0m, consulta.ValorTotal);
        Assert.Equal(0.0m, consulta.ValorCofins);
        Assert.Equal(0.0m, consulta.ValorCsll);
        Assert.Equal(0.0m, consulta.ValorInss);
        Assert.Equal(0.0m, consulta.ValorIr);
        Assert.Equal(0.0m, consulta.ValorPis);

        Assert.NotNull(consulta.Cliente);
        Assert.Equal("F", consulta.Cliente!.TipoPessoa);
        Assert.Equal("Cliente teste", consulta.Cliente.Nome);
        Assert.Equal("teste@teste.com.br", consulta.Cliente.Email);
        Assert.Equal("38618699772", consulta.Cliente.CpfCnpj);

        Assert.NotNull(consulta.Servico);
        Assert.Equal("Teste webservice", consulta.Servico!.Descricao);
        Assert.Equal(3.0m, consulta.Servico.AliquotaIss);
        Assert.False(consulta.Servico.IssRetidoFonte);
        Assert.Equal("4.12", consulta.Servico.CodigoServicoMunicipio);
        Assert.Equal("4.12", consulta.Servico.ItemListaServicoLC116);
        Assert.Equal("8630504", consulta.Servico.Cnae);
        Assert.Equal("3300704", consulta.Servico.MunicipioPrestacaoServico);
    }

    [Fact]
    public void Deserialize_MotivoStatus_StringWhenNegada()
    {
        const string json = """
            {
              "id": "teste-unitario-nfse-negada",
              "tipo": "NFS-e",
              "status": "Negada",
              "motivoStatus": "Rejeicao da prefeitura (teste)"
            }
            """;

        var consulta = JsonConvert.DeserializeObject<ConsultaNfse>(json);

        Assert.NotNull(consulta);
        Assert.Equal("Negada", consulta!.Status);
        Assert.Equal("Rejeicao da prefeitura (teste)", consulta.MotivoStatus);
    }

    [Fact]
    public void Deserialize_ListaNfse_PopulatesTotalRecordsAndData()
    {
        var json = FixtureLoader.Read("lista-nfse.json");

        var lista = JsonConvert.DeserializeObject<ListaNfse>(json);

        Assert.NotNull(lista);
        Assert.Equal(1, lista!.TotalRecords);
        Assert.NotNull(lista.Data);
        Assert.Single(lista.Data!);
        Assert.Equal("teste-unitario-nfse-001", lista.Data[0].Id);
        Assert.Equal("NFS-e", lista.Data[0].Tipo);
        Assert.Equal("TESTE231024", lista.Data[0].IdExterno);
        Assert.Equal("Autorizada", lista.Data[0].Status);
        Assert.Null(lista.Data[0].MotivoStatus);
        Assert.Equal("100", lista.Data[0].Numero);
        Assert.Equal(1.0m, lista.Data[0].ValorTotal);
    }
}
