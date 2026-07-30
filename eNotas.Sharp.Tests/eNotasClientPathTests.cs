using System.Net;
using eNotas.Sharp.Clients;
using eNotas.Sharp.Models;
using eNotas.Sharp.Tests.Helpers;

namespace eNotas.Sharp.Tests;

public class eNotasClientPathTests
{
    private const string ApiKey = "test-api-key";
    private const string EmpresaId = "empresa-teste";

    [Fact]
    public async Task EmitirNfe_PostsToExpectedPathWithAuthAndBody()
    {
        var handler = new FakeHandler
        {
            Responder = _ => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"nfeId\":\"ok\"}")
            }
        };

        using var client = new eNotasClient(ApiKey, handler);
        var nota = new Nota { Id = "nota-1", Tipo = "NF-e", ValorTotal = 1m };

        var response = await client.EmitirNfe(nota, EmpresaId);

        Assert.True(response.IsSuccess);
        Assert.Equal("OK", response.Status);
        Assert.Equal("{\"nfeId\":\"ok\"}", response.Message);
        Assert.NotNull(handler.LastRequest);
        Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
        Assert.EndsWith($"/v2/empresas/{EmpresaId}/nf-e", handler.LastRequest.RequestUri!.AbsolutePath);
        Assert.Equal($"Basic {ApiKey}", handler.LastRequest.Headers.GetValues("Authorization").Single());
        Assert.Contains("\"id\":\"nota-1\"", handler.LastContent);
        Assert.Contains("\"tipo\":\"NF-e\"", handler.LastContent);
    }

    [Fact]
    public async Task EmitirNfce_PostsToNfcePath()
    {
        var handler = new FakeHandler
        {
            Responder = _ => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}")
            }
        };

        using var client = new eNotasClient(ApiKey, handler);
        var response = await client.EmitirNfce(new Nota { Id = "nfce-1" }, EmpresaId);

        Assert.True(response.IsSuccess);
        Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
        Assert.EndsWith($"/v2/empresas/{EmpresaId}/nfc-e", handler.LastRequest.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task ConsultaNfe_GetsTypedConsulta()
    {
        var consultaJson = FixtureLoader.Read("consulta-nfe.json");
        var handler = new FakeHandler
        {
            Responder = _ => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(consultaJson)
            }
        };

        using var client = new eNotasClient(ApiKey, handler);
        var response = await client.ConsultaNfe("nota-123", EmpresaId);

        Assert.True(response.IsSuccess);
        Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
        Assert.EndsWith($"/v2/empresas/{EmpresaId}/nf-e/nota-123", handler.LastRequest.RequestUri!.AbsolutePath);
        Assert.NotNull(response.Object);
        Assert.Equal("Autorizada", response.Object!.Status);
        Assert.Equal("teste-unitario-001", response.Object.Id);
    }

    [Fact]
    public async Task CancelaNfe_DeletesExpectedPath()
    {
        var handler = new FakeHandler
        {
            Responder = _ => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"ok\":true}")
            }
        };

        using var client = new eNotasClient(ApiKey, handler);
        var response = await client.CancelaNfe("nota-123", EmpresaId);

        Assert.True(response.IsSuccess);
        Assert.Equal(HttpMethod.Delete, handler.LastRequest!.Method);
        Assert.Contains($"/v2/empresas/{EmpresaId}/nf-e/nota-123", handler.LastRequest.RequestUri!.AbsoluteUri);
        Assert.Equal($"Basic {ApiKey}", handler.LastRequest.Headers.GetValues("Authorization").First());
    }

    [Fact]
    public async Task EmitirNfe_OnBadRequest_SetsIsSuccessFalseAndMessage()
    {
        var handler = new FakeHandler
        {
            Responder = _ => new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("{\"mensagem\":\"erro de validacao\"}")
            }
        };

        using var client = new eNotasClient(ApiKey, handler);
        var response = await client.EmitirNfe(new Nota { Id = "x" }, EmpresaId);

        Assert.False(response.IsSuccess);
        Assert.Equal("BadRequest", response.Status);
        Assert.Equal("{\"mensagem\":\"erro de validacao\"}", response.Message);
    }
}
