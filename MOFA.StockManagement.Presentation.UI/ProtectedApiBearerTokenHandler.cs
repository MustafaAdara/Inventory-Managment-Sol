using System.Net;

public class ProtectedApiBearerTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _contextAccessor;

    public ProtectedApiBearerTokenHandler(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var accessToken = _contextAccessor.HttpContext?.Session.GetString("Token");
            request.Headers.Remove("Authorization");
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            return await base.SendAsync(request, cancellationToken);
        }
        catch
        {
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}