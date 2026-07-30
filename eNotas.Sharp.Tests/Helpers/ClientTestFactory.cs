using System.Net;
using eNotas.Sharp.Clients;

namespace eNotas.Sharp.Tests.Helpers;

internal static class ClientTestFactory
{
    public const string ApiKey = "test-api-key";
    public const string EmpresaId = "empresa-teste";
    public const string NotaId = "nota-123";
    public const string InutilizacaoId = "inut-123";
    public const string CartaId = "cce-123";

    public static (eNotasClient Client, FakeHandler Handler) Create(
        Func<HttpRequestMessage, HttpResponseMessage>? responder = null)
    {
        var handler = new FakeHandler();
        if (responder != null)
            handler.Responder = responder;

        return (new eNotasClient(ApiKey, handler), handler);
    }

    public static HttpResponseMessage Ok(string content) =>
        new(HttpStatusCode.OK) { Content = new StringContent(content) };
}
