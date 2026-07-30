using System.Net;
using eNotas.Sharp.Models;
using eNotas.Sharp.Services;
using eNotas.Sharp.Tests.Helpers;

namespace eNotas.Sharp.Tests;

public class RestServiceTests
{
    private const string BaseUrl = "https://api.enotasgw.com.br";
    private const string ApiKey = "test-api-key";

    [Fact]
    public async Task Get_InvalidJson_LeavesObjectNull()
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
        Assert.Null(response.Exception);
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
}
