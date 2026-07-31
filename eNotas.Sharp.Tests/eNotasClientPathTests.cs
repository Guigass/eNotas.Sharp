using System.Net;
using eNotas.Sharp.Models;
using eNotas.Sharp.Tests.Helpers;

namespace eNotas.Sharp.Tests;

public class eNotasClientPathTests
{
    [Fact]
    public async Task EmitirNfe_PostsToExpectedPathWithAuthAndBody()
    {
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok("{\"nfeId\":\"ok\"}"));
        using (client)
        {
            var response = await client.EmitirNfe(new Nota { Id = "nota-1", Tipo = "NF-e", ValorTotal = 1m }, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.Equal("OK", response.Status);
            Assert.Equal("{\"nfeId\":\"ok\"}", response.Message);
            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nf-e", handler.LastRequest.RequestUri!.AbsolutePath);
            Assert.Equal($"Basic {ClientTestFactory.ApiKey}", handler.LastRequest.Headers.GetValues("Authorization").Single());
            Assert.Contains("\"id\":\"nota-1\"", handler.LastContent);
            Assert.Contains("\"tipo\":\"NF-e\"", handler.LastContent);
        }
    }

    [Fact]
    public async Task EmitirNfce_PostsToNfcePath()
    {
        var (client, handler) = ClientTestFactory.Create();
        using (client)
        {
            var response = await client.EmitirNfce(new Nota { Id = "nfce-1" }, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nfc-e", handler.LastRequest.RequestUri!.AbsolutePath);
        }
    }

    [Fact]
    public async Task IncluirAlterarEmpresa_PostsToExpectedPathWithAuthAndBody()
    {
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok("{\"empresaId\":\"emp-1\"}"));
        using (client)
        {
            var response = await client.IncluirAlterarEmpresa(new Empresa
            {
                Cnpj = "99999999999999",
                RazaoSocial = "Cliente Teste"
            });

            Assert.True(response.IsSuccess);
            Assert.Equal("OK", response.Status);
            Assert.Equal("{\"empresaId\":\"emp-1\"}", response.Message);
            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.EndsWith("/v2/empresas", handler.LastRequest.RequestUri!.AbsolutePath);
            Assert.Equal($"Basic {ClientTestFactory.ApiKey}", handler.LastRequest.Headers.GetValues("Authorization").Single());
            Assert.Contains("\"cnpj\":\"99999999999999\"", handler.LastContent);
            Assert.Contains("\"razaoSocial\":\"Cliente Teste\"", handler.LastContent);
        }
    }

    [Fact]
    public async Task ConsultaEmpresa_GetsTypedEmpresa()
    {
        var json = "{\"id\":\"empresa-teste\",\"cnpj\":\"99999999999999\",\"razaoSocial\":\"Cliente Teste\"}";
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok(json));
        using (client)
        {
            var response = await client.ConsultaEmpresa(ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}", handler.LastRequest.RequestUri!.AbsolutePath);
            Assert.Equal($"Basic {ClientTestFactory.ApiKey}", handler.LastRequest.Headers.GetValues("Authorization").Single());
            Assert.Equal("empresa-teste", response.Object!.Id);
            Assert.Equal("99999999999999", response.Object.Cnpj);
            Assert.Equal("Cliente Teste", response.Object.RazaoSocial);
        }
    }

    [Fact]
    public async Task ListarEmpresas_GetsTypedListaWithQueryParams()
    {
        const string json = """
            {
              "totalRecords": 1,
              "data": [
                {
                  "id": "empresa-lista",
                  "cnpj": "99999999999999",
                  "razaoSocial": "Cliente Teste"
                }
              ]
            }
            """;
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok(json));
        using (client)
        {
            var response = await client.ListarEmpresas(
                pageNumber: 0,
                pageSize: 5,
                searchBy: "cidade",
                searchTerm: "São Paulo",
                sortBy: "nome_fantasia",
                sortDirection: "asc");

            Assert.True(response.IsSuccess);
            Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
            Assert.EndsWith("/v2/empresas", handler.LastRequest.RequestUri!.AbsolutePath);
            Assert.Equal($"Basic {ClientTestFactory.ApiKey}", handler.LastRequest.Headers.GetValues("Authorization").Single());

            var query = handler.LastRequest.RequestUri.Query;
            Assert.Contains("pageNumber=0", query);
            Assert.Contains("pageSize=5", query);
            Assert.Contains("searchBy=cidade", query);
            Assert.Contains("searchTerm=S%C3%A3o%20Paulo", query);
            Assert.Contains("sortBy=nome_fantasia", query);
            Assert.Contains("sortDirection=asc", query);

            Assert.Equal(1, response.Object!.TotalRecords);
            Assert.NotNull(response.Object.Data);
            Assert.Single(response.Object.Data!);
            Assert.Equal("empresa-lista", response.Object.Data[0].Id);
            Assert.Equal("99999999999999", response.Object.Data[0].Cnpj);
            Assert.Equal("Cliente Teste", response.Object.Data[0].RazaoSocial);
        }
    }

    [Fact]
    public async Task VincularCertificadoDigital_PostsMultipartToExpectedPathWithAuth()
    {
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok("{}"));
        using (client)
        {
            var arquivo = new byte[] { 0x01, 0x02, 0x03 };
            var response = await client.VincularCertificadoDigital(
                ClientTestFactory.EmpresaId,
                arquivo,
                "senha-teste",
                "cert.pfx");

            Assert.True(response.IsSuccess);
            Assert.Equal("OK", response.Status);
            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.EndsWith(
                $"/v2/empresas/{ClientTestFactory.EmpresaId}/certificadoDigital",
                handler.LastRequest.RequestUri!.AbsolutePath);
            Assert.Equal($"Basic {ClientTestFactory.ApiKey}", handler.LastRequest.Headers.GetValues("Authorization").Single());
            Assert.StartsWith("multipart/form-data", handler.LastRequest.Content!.Headers.ContentType!.MediaType);
            Assert.Contains("senha", handler.LastContent);
            Assert.Contains("arquivo", handler.LastContent);
            Assert.Contains("cert.pfx", handler.LastContent);
        }
    }

    [Fact]
    public async Task VincularLogotipo_PostsMultipartToExpectedPathWithAuth()
    {
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok("{}"));
        using (client)
        {
            var arquivo = new byte[] { 0x89, 0x50, 0x4E, 0x47 };
            var response = await client.VincularLogotipo(
                ClientTestFactory.EmpresaId,
                arquivo,
                "logo.png");

            Assert.True(response.IsSuccess);
            Assert.Equal("OK", response.Status);
            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.EndsWith(
                $"/v2/empresas/{ClientTestFactory.EmpresaId}/logo",
                handler.LastRequest.RequestUri!.AbsolutePath);
            Assert.Equal($"Basic {ClientTestFactory.ApiKey}", handler.LastRequest.Headers.GetValues("Authorization").Single());
            Assert.StartsWith("multipart/form-data", handler.LastRequest.Content!.Headers.ContentType!.MediaType);
            Assert.Contains("logotipo", handler.LastContent);
            Assert.Contains("logo.png", handler.LastContent);
        }
    }

    [Fact]
    public async Task DesabilitarEmpresa_PostsToExpectedPathWithAuth()
    {
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok("{}"));
        using (client)
        {
            var response = await client.DesabilitarEmpresa(ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.Equal("OK", response.Status);
            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.EndsWith(
                $"/v1/empresas/{ClientTestFactory.EmpresaId}/desabilitar",
                handler.LastRequest.RequestUri!.AbsolutePath);
            Assert.Equal($"Basic {ClientTestFactory.ApiKey}", handler.LastRequest.Headers.GetValues("Authorization").Single());
            Assert.Null(handler.LastRequest.Content);
        }
    }

    [Fact]
    public async Task HabilitarEmpresa_PostsToExpectedPathWithAuth()
    {
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok("{}"));
        using (client)
        {
            var response = await client.HabilitarEmpresa(ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.Equal("OK", response.Status);
            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.EndsWith(
                $"/v1/empresas/{ClientTestFactory.EmpresaId}/habilitar",
                handler.LastRequest.RequestUri!.AbsolutePath);
            Assert.Equal($"Basic {ClientTestFactory.ApiKey}", handler.LastRequest.Headers.GetValues("Authorization").Single());
            Assert.Null(handler.LastRequest.Content);
        }
    }

    [Fact]
    public async Task ConsultaNfe_GetsTypedConsulta()
    {
        var json = FixtureLoader.Read("consulta-nfe.json");
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok(json));
        using (client)
        {
            var response = await client.ConsultaNfe(ClientTestFactory.NotaId, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nf-e/{ClientTestFactory.NotaId}", handler.LastRequest.RequestUri!.AbsolutePath);
            Assert.Equal("Autorizada", response.Object!.Status);
            Assert.Equal("teste-unitario-001", response.Object.Id);
        }
    }

    [Fact]
    public async Task CancelaNfe_DeletesExpectedPath()
    {
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok("{\"ok\":true}"));
        using (client)
        {
            var response = await client.CancelaNfe(ClientTestFactory.NotaId, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.Equal(HttpMethod.Delete, handler.LastRequest!.Method);
            Assert.Contains($"/v2/empresas/{ClientTestFactory.EmpresaId}/nf-e/{ClientTestFactory.NotaId}", handler.LastRequest.RequestUri!.AbsoluteUri);
            Assert.Equal($"Basic {ClientTestFactory.ApiKey}", handler.LastRequest.Headers.GetValues("Authorization").First());
        }
    }

    [Fact]
    public async Task EmitirNfe_OnBadRequest_SetsIsSuccessFalseAndMessage()
    {
        var (client, _) = ClientTestFactory.Create(_ =>
            new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("{\"mensagem\":\"erro de validacao\"}")
            });
        using (client)
        {
            var response = await client.EmitirNfe(new Nota { Id = "x" }, ClientTestFactory.EmpresaId);

            Assert.False(response.IsSuccess);
            Assert.Equal("BadRequest", response.Status);
            Assert.Equal("{\"mensagem\":\"erro de validacao\"}", response.Message);
        }
    }

    [Fact]
    public async Task ConsultaNfeXML_GetsNfeProc()
    {
        var xml = FixtureLoader.Read("nfe-proc-minimo.xml");
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok(xml));
        using (client)
        {
            var response = await client.ConsultaNfeXML(ClientTestFactory.NotaId, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nf-e/{ClientTestFactory.NotaId}/xml", handler.LastRequest.RequestUri!.AbsolutePath);
            Assert.NotNull(response.Object);
            Assert.Equal("4.00", response.Object!.Versao);
        }
    }

    [Fact]
    public async Task ConsultaNfeXMLCancelamento_GetsProcEvento()
    {
        var xml = FixtureLoader.Read("proc-evento-cancelamento-minimo.xml");
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok(xml));
        using (client)
        {
            var response = await client.ConsultaNfeXMLCancelamento(ClientTestFactory.NotaId, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nf-e/{ClientTestFactory.NotaId}/xmlCancelamento", handler.LastRequest!.RequestUri!.AbsolutePath);
            Assert.NotNull(response.Object);
            Assert.Equal("110111", response.Object!.Evento!.InfEvento!.TpEvento);
        }
    }

    [Fact]
    public async Task InutilizacaoNfe_PostsBodyToPath()
    {
        var (client, handler) = ClientTestFactory.Create();
        using (client)
        {
            var inutilizacao = new Inutilizacao
            {
                Id = "inut-1",
                AmbienteEmissao = "Homologacao",
                Serie = "1",
                NumeroInicial = 100,
                NumeroFinal = 105,
                Justificativa = "teste"
            };

            var response = await client.InutilizacaoNfe(inutilizacao, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nf-e/inutilizacao", handler.LastRequest.RequestUri!.AbsolutePath);
            Assert.Contains("\"id\":\"inut-1\"", handler.LastContent);
            Assert.Contains("\"numeroInicial\":100", handler.LastContent);
        }
    }

    [Fact]
    public async Task ConsultaInutilizacaoNfe_GetsTypedResponse()
    {
        var json = FixtureLoader.Read("consulta-inutilizacao.json");
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok(json));
        using (client)
        {
            var response = await client.ConsultaInutilizacaoNfe(ClientTestFactory.InutilizacaoId, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nf-e/inutilizacao/{ClientTestFactory.InutilizacaoId}", handler.LastRequest!.RequestUri!.AbsolutePath);
            Assert.Equal("Autorizada", response.Object!.Status);
            Assert.Equal(100, response.Object.NumeroInicial);
        }
    }

    [Fact]
    public async Task ConsultaInutilizacaoXMLNfe_GetsProcInut()
    {
        var xml = FixtureLoader.Read("proc-inut-minimo.xml");
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok(xml));
        using (client)
        {
            var response = await client.ConsultaInutilizacaoXMLNfe(ClientTestFactory.InutilizacaoId, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nf-e/inutilizacao/{ClientTestFactory.InutilizacaoId}/xml", handler.LastRequest!.RequestUri!.AbsolutePath);
            Assert.NotNull(response.Object);
            Assert.Equal("4.00", response.Object!.Versao);
            Assert.Equal("100", response.Object.InutNFe!.InfInut!.NNFIni);
        }
    }

    [Fact]
    public async Task CartaDeCorrecao_PostsBodyToPath()
    {
        var (client, handler) = ClientTestFactory.Create();
        using (client)
        {
            var carta = new CartaCorrecao
            {
                Id = "cce-1",
                AmbienteEmissao = "Homologacao",
                Numero = 1,
                Correcao = "texto",
                Nfe = new Nfe { ChaveAcesso = "00000000000000000000000000000000000000000000" }
            };

            var response = await client.CartaDeCorrecao(carta, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nf-e/cartaCorrecao", handler.LastRequest.RequestUri!.AbsolutePath);
            Assert.Contains("\"correcao\":\"texto\"", handler.LastContent);
        }
    }

    [Fact]
    public async Task ConsultaCartaDeCorrecao_GetsTypedResponse()
    {
        var json = FixtureLoader.Read("correcao-response.json");
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok(json));
        using (client)
        {
            var response = await client.ConsultaCartaDeCorrecao(ClientTestFactory.CartaId, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nf-e/cartaCorrecao/{ClientTestFactory.CartaId}", handler.LastRequest!.RequestUri!.AbsolutePath);
            Assert.Equal("Autorizada", response.Object!.Status);
            Assert.Equal(1, response.Object.Numero);
        }
    }

    [Fact]
    public async Task ConsultaCartaDeCorrecaoXml_GetsProcEvento()
    {
        var xml = FixtureLoader.Read("proc-evento-correcao-minimo.xml");
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok(xml));
        using (client)
        {
            var response = await client.ConsultaCartaDeCorrecaoXml(ClientTestFactory.CartaId, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nf-e/cartaCorrecao/{ClientTestFactory.CartaId}/xml", handler.LastRequest!.RequestUri!.AbsolutePath);
            Assert.NotNull(response.Object);
            Assert.Equal("110110", response.Object!.Evento!.InfEvento!.TpEvento);
            Assert.Equal("Correcao de teste", response.Object.Evento.InfEvento.DetEvento!.XCorrecao);
        }
    }

    [Fact]
    public async Task ConsultaNfce_GetsTypedConsulta()
    {
        var json = FixtureLoader.Read("consulta-nfe.json");
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok(json));
        using (client)
        {
            var response = await client.ConsultaNfce(ClientTestFactory.NotaId, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nfc-e/{ClientTestFactory.NotaId}", handler.LastRequest!.RequestUri!.AbsolutePath);
            Assert.NotNull(response.Object);
        }
    }

    [Fact]
    public async Task ConsultaNfceXML_GetsNfeProc()
    {
        var xml = FixtureLoader.Read("nfe-proc-minimo.xml");
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok(xml));
        using (client)
        {
            var response = await client.ConsultaNfceXML(ClientTestFactory.NotaId, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nfc-e/{ClientTestFactory.NotaId}/xml", handler.LastRequest!.RequestUri!.AbsolutePath);
            Assert.NotNull(response.Object);
        }
    }

    [Fact]
    public async Task ConsultaNfceXMLCancelamento_GetsProcEvento()
    {
        var xml = FixtureLoader.Read("proc-evento-cancelamento-minimo.xml");
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok(xml));
        using (client)
        {
            var response = await client.ConsultaNfceXMLCancelamento(ClientTestFactory.NotaId, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nfc-e/{ClientTestFactory.NotaId}/xmlCancelamento", handler.LastRequest!.RequestUri!.AbsolutePath);
            Assert.NotNull(response.Object);
        }
    }

    [Fact]
    public async Task CancelaNfce_DeletesExpectedPath()
    {
        var (client, handler) = ClientTestFactory.Create();
        using (client)
        {
            var response = await client.CancelaNfce(ClientTestFactory.NotaId, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.Equal(HttpMethod.Delete, handler.LastRequest!.Method);
            Assert.Contains($"/v2/empresas/{ClientTestFactory.EmpresaId}/nfc-e/{ClientTestFactory.NotaId}", handler.LastRequest.RequestUri!.AbsoluteUri);
        }
    }

    [Fact]
    public async Task InutilizacaoNfce_PostsToPath()
    {
        var (client, handler) = ClientTestFactory.Create();
        using (client)
        {
            var response = await client.InutilizacaoNfce(
                new Inutilizacao { Id = "inut-nfce", Serie = "1", NumeroInicial = 1, NumeroFinal = 2, Justificativa = "teste" },
                ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nfc-e/inutilizacao", handler.LastRequest.RequestUri!.AbsolutePath);
        }
    }

    [Fact]
    public async Task ConsultaInutilizacaoNfce_GetsTypedResponse()
    {
        var json = FixtureLoader.Read("consulta-inutilizacao.json");
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok(json));
        using (client)
        {
            var response = await client.ConsultaInutilizacaoNfce(ClientTestFactory.InutilizacaoId, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nfc-e/inutilizacao/{ClientTestFactory.InutilizacaoId}", handler.LastRequest!.RequestUri!.AbsolutePath);
            Assert.NotNull(response.Object);
        }
    }

    [Fact]
    public async Task ConsultaInutilizacaoXMLNfce_GetsProcInut()
    {
        var xml = FixtureLoader.Read("proc-inut-minimo.xml");
        var (client, handler) = ClientTestFactory.Create(_ => ClientTestFactory.Ok(xml));
        using (client)
        {
            var response = await client.ConsultaInutilizacaoXMLNfce(ClientTestFactory.InutilizacaoId, ClientTestFactory.EmpresaId);

            Assert.True(response.IsSuccess);
            Assert.EndsWith($"/v2/empresas/{ClientTestFactory.EmpresaId}/nfc-e/inutilizacao/{ClientTestFactory.InutilizacaoId}/xml", handler.LastRequest!.RequestUri!.AbsolutePath);
            Assert.NotNull(response.Object);
        }
    }

    [Fact]
    public async Task EmitirNfe_WhenTokenAlreadyCanceled_ThrowsOperationCanceledException()
    {
        var (client, _) = ClientTestFactory.Create();
        using (client)
        using (var cts = new CancellationTokenSource())
        {
            cts.Cancel();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
                client.EmitirNfe(new Nota { Id = "x" }, ClientTestFactory.EmpresaId, cts.Token));
        }
    }
}
