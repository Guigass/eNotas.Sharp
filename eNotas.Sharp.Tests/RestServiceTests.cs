using System.Net;
using eNotas.Sharp.Models;
using eNotas.Sharp.Models.Xml;
using eNotas.Sharp.Services;
using eNotas.Sharp.Tests.Helpers;

namespace eNotas.Sharp.Tests;

public class RestServiceTests
{
    private const string BaseUrl = "https://api.enotasgw.com.br";
    private const string ApiKey = "test-api-key";

    [Fact]
    public async Task Get_InvalidJson_LeavesObjectNull_AndSetsException()
    {
        var handler = new FakeHandler
        {
            Responder = _ => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{invalid-json")
            }
        };

        using var service = new RestService(BaseUrl, ApiKey, handler);

        var response = await service.Get<Consulta>("/v2/empresas/x/nf-e/y");

        Assert.True(response.IsSuccess);
        Assert.Equal("OK", response.Status);
        Assert.Null(response.Object);
        Assert.Equal("{invalid-json", response.Message);
        Assert.NotNull(response.Exception);
    }

    [Fact]
    public async Task Get_InvalidXml_LeavesObjectNull_AndSetsException()
    {
        var handler = new FakeHandler
        {
            Responder = _ => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("not-xml")
            }
        };

        using var service = new RestService(BaseUrl, ApiKey, handler);

        var response = await service.Get<NfeProc>("/v2/empresas/x/nf-e/y/xml", "xml");

        Assert.True(response.IsSuccess);
        Assert.Equal("OK", response.Status);
        Assert.Null(response.Object);
        Assert.Equal("not-xml", response.Message);
        Assert.NotNull(response.Exception);
    }

    [Fact]
    public async Task Post_WhenHandlerThrows_SetsExceptionWithoutRethrow()
    {
        var handler = new FakeHandler
        {
            Responder = _ => throw new HttpRequestException("falha de rede simulada")
        };

        using var service = new RestService(BaseUrl, ApiKey, handler);

        var response = await service.Post("/v2/empresas/x/nf-e", new Nota { Id = "1" });

        Assert.False(response.IsSuccess);
        Assert.NotNull(response.Exception);
        Assert.Contains("falha de rede simulada", response.Exception!.Message);
    }

    [Fact]
    public async Task Put_SendsPutToExpectedPath()
    {
        var handler = new FakeHandler
        {
            Responder = _ => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"ok\":true}")
            }
        };

        using var service = new RestService(BaseUrl, ApiKey, handler);

        var response = await service.Put("/v2/empresas/x/recurso");

        Assert.True(response.IsSuccess);
        Assert.Equal(HttpMethod.Put, handler.LastRequest!.Method);
        Assert.EndsWith("/v2/empresas/x/recurso", handler.LastRequest.RequestUri!.AbsolutePath);
        Assert.Equal("{\"ok\":true}", response.Message);
    }

    [Fact]
    public async Task Delete_SendsDeleteToExpectedPath()
    {
        var handler = new FakeHandler
        {
            Responder = _ => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"ok\":true}")
            }
        };

        using var service = new RestService(BaseUrl, ApiKey, handler);

        var response = await service.Delete("/v2/empresas/x/nf-e/y");

        Assert.True(response.IsSuccess);
        Assert.Equal(HttpMethod.Delete, handler.LastRequest!.Method);
        Assert.EndsWith("/v2/empresas/x/nf-e/y", handler.LastRequest.RequestUri!.AbsolutePath);
        Assert.DoesNotContain("//v2", handler.LastRequest.RequestUri.AbsoluteUri);
        Assert.Equal("{\"ok\":true}", response.Message);
    }

    [Fact]
    public async Task Post_WhenTokenAlreadyCanceled_ThrowsOperationCanceledException()
    {
        var handler = new FakeHandler();
        using var service = new RestService(BaseUrl, ApiKey, handler);
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            service.Post("/v2/empresas/x/nf-e", new Nota { Id = "1" }, cts.Token));
    }

    [Fact]
    public async Task GetBytes_Success_ReturnsBytesInObject_AndLeavesMessageNull()
    {
        var pdfBytes = new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D }; // %PDF-
        var handler = new FakeHandler
        {
            Responder = _ =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(pdfBytes)
                };
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
                return response;
            }
        };

        using var service = new RestService(BaseUrl, ApiKey, handler);

        var response = await service.GetBytes("/v1/empresas/x/nfes/y/pdf");

        Assert.True(response.IsSuccess);
        Assert.Equal("OK", response.Status);
        Assert.Equal(pdfBytes, response.Object);
        Assert.Null(response.Message);
        Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
        Assert.EndsWith("/v1/empresas/x/nfes/y/pdf", handler.LastRequest.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task GetBytes_HttpError_LeavesObjectNull_AndSetsMessageFromBody()
    {
        const string errorJson = "{\"message\":\"nota nao encontrada\"}";
        var handler = new FakeHandler
        {
            Responder = _ => new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(errorJson)
            }
        };

        using var service = new RestService(BaseUrl, ApiKey, handler);

        var response = await service.GetBytes("/v1/empresas/x/nfes/y/pdf");

        Assert.False(response.IsSuccess);
        Assert.Equal("NotFound", response.Status);
        Assert.Null(response.Object);
        Assert.Equal(errorJson, response.Message);
    }

    [Fact]
    public async Task GetBytes_WhenTokenAlreadyCanceled_ThrowsOperationCanceledException()
    {
        var handler = new FakeHandler();
        using var service = new RestService(BaseUrl, ApiKey, handler);
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            service.GetBytes("/v1/empresas/x/nfes/y/pdf", cts.Token));
    }
}
