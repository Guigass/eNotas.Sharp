using System.Net;

namespace eNotas.Sharp.Tests.Helpers;

internal sealed class FakeHandler : HttpMessageHandler
{
    public HttpRequestMessage? LastRequest { get; private set; }
    public string? LastContent { get; private set; }
    public Func<HttpRequestMessage, HttpResponseMessage> Responder { get; set; } =
        _ => new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("{}")
        };

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        LastRequest = request;

        if (request.Content != null)
            LastContent = await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        return Responder(request);
    }
}
