using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace MOFA.StockManagement.Infrastructure.Business.Extension
{
    public interface IHttpClientHelper<T> : IDisposable where T : notnull
    {
        Task<T?> GetTokenAsync(string url, string clientId, string secretKey, Dictionary<string, string>? headers = null, CancellationToken token = default);
        Task<T?> FindAsync(string url, string? accessToken = null, CancellationToken token = default);

        Task<TResult?> FindAsync<TResult>(string url,
            T postObject,
            string? accessToken = null,
            CancellationToken token = default);

        Task<MemoryStream> DownaloadAsync(string url, string? accessToken = null, CancellationToken token = default);
        Task<IList<T>?> SelectAsync(string url, string? accessToken = null, CancellationToken token = default);

        Task<IList<TResult>?> SelectAsync<TResult>(string url,
            T postObject,
            string? accessToken = null,
            CancellationToken token = default) where TResult : class;

        Task<T?> AddAsync(string url, T postObject, string? accessToken = null, CancellationToken token = default);


        Task<TResult?> AddAsync<TResult>(string url, T postObject, string? accessToken = null,
            CancellationToken token = default);

        Task<TResult?> AddAsync<TResult>(string url, string postJson, string? accessToken = null,
            CancellationToken token = default);
        Task ModifyAsync(string url, T putObject, string? accessToken = null, CancellationToken token = default);

        Task<TResult?> ModifyAsync<TResult>(string url, T putObject, string? accessToken = null,
            CancellationToken token = default);

        Task DeleteAsync(string url, string? accessToken = null, CancellationToken token = default);
    }

    public class HttpClientHelper<T> : IHttpClientHelper<T>, IDisposable where T : notnull
    {
        private readonly HttpClient _client;
        private readonly ISerializeService _serializeService;

        public HttpClientHelper(ISerializeService serializeService)
        {
            _client = new HttpClient();
            _serializeService = serializeService;
        }

        public async Task<T?> GetTokenAsync(string url, string clientId, string secretKey, Dictionary<string, string>? headers = null, CancellationToken token = default)
        {
            T? result;

            var input = $"grant_type=client_credentials&client_id={clientId}&client_secret={secretKey}&scope=InvoicingAPI";
            var postContent = new StringContent(input, Encoding.UTF8, "application/x-www-form-urlencoded");

            if (headers != null)
                foreach (KeyValuePair<string, string> entry in headers)
                    _client.DefaultRequestHeaders.Add(entry.Key, entry.Value);

            var response = await _client.PostAsync(url, postContent, token);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(token);
                result = _serializeService.DeserializeJson<T>(content);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException(content, null, response.StatusCode);
            }

            return result;
        }

        #region FindAsync

        public async Task<TResult?> FindAsync<TResult>(string url, T postObject, string? accessToken = null, CancellationToken token = default)
        {
            var result = default(TResult);
            var json = _serializeService.SerializeJson(postObject);
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.PostAsync(url, postContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {

                    if (typeof(TResult).Namespace != "System")
                        result = _serializeService.DeserializeJson<TResult>(x.Result);
                    else
                        result = (TResult?)Convert.ChangeType(x?.Result, typeof(TResult));
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException(content, null, response.StatusCode);
            }

            return result;
        }

        public async Task<T?> FindAsync(string url, string? accessToken = null, CancellationToken token = default)
        {
            var result = default(T);

            if (!string.IsNullOrEmpty(accessToken))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.GetAsync(url, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return result;

                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    if (typeof(T).Namespace != "System")
                        result = _serializeService.DeserializeJson<T>(x.Result);
                    else
                        result = (T?)Convert.ChangeType(x?.Result, typeof(T));
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException(content, null, response.StatusCode);
            }

            return result;
        }

        #endregion

        #region SelectAsync
        public async Task<MemoryStream> DownaloadAsync(string url, string? accessToken = null, CancellationToken token = default)
        {
            if (!string.IsNullOrEmpty(accessToken))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.GetAsync(url, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await using var ms = new MemoryStream();
                await response.Content.CopyToAsync(ms, token);
                return ms;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException(content, null, response.StatusCode);
            }
        }

        public async Task<IList<T>?> SelectAsync(string url, string? accessToken = null, CancellationToken token = default)
        {
            IList<T>? result = null;

            if (!string.IsNullOrEmpty(accessToken))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.GetAsync(url, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    result = _serializeService.DeserializeJson<IList<T>>(x.Result);
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException(content, null, response.StatusCode);
            }

            return result;

        }

        public async Task<IList<TResult>?> SelectAsync<TResult>(string url, T postObject, string? accessToken = null, CancellationToken token = default) where TResult : class
        {
            IList<TResult>? result = null;

            var json = _serializeService.SerializeJson(postObject);
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.PostAsync(url, postContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    result = _serializeService.DeserializeJson<IList<TResult>>(x.Result);
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException(content, null, response.StatusCode);
            }

            return result;
        }

        #endregion

        #region DeleteAysnc

        public async Task DeleteAsync(string url, string? accessToken = null, CancellationToken token = default)
        {
            if (!string.IsNullOrEmpty(accessToken))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.DeleteAsync(url, token).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException(content, null, response.StatusCode);
            }
        }


        #endregion

        #region AddAysnc

        public async Task<T?> AddAsync(string url, T postObject, string? accessToken = null, CancellationToken token = default)
        {
            T? result = default;

            var json = _serializeService.SerializeJson(postObject);
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.PostAsync(url, postContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    result = _serializeService.DeserializeJson<T>(x.Result);
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException(content, null, response.StatusCode);
            }

            return result;
        }

        public async Task<TResult?> AddAsync<TResult>(string url, T postObject, string? accessToken = null, CancellationToken token = default)
        {
            TResult? result = default;

            var json = _serializeService.SerializeJson(postObject);
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.PostAsync(url, postContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    result = _serializeService.DeserializeJson<TResult>(x.Result);
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException(content, null, response.StatusCode);
            }

            return result;
        }
        public async Task<TResult?> AddAsync<TResult>(string url, string postJson, string? accessToken = null, CancellationToken token = default)
        {
            TResult? result = default;

            //var json = _serializeService.SerializeJson(postObject);
            var postContent = new StringContent(postJson, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.PostAsync(url, postContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    result = _serializeService.DeserializeJson<TResult>(x.Result);
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException(content, null, response.StatusCode);
            }

            return result;
        }

        #endregion

        #region ModifyAsync

        public async Task ModifyAsync(string url, T putObject, string? accessToken = null, CancellationToken token = default)
        {
            var json = _serializeService.SerializeJson(putObject);
            var putContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.PutAsync(url, putContent, token).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException(content, null, response.StatusCode);
            }
        }

        public async Task<TResult?> ModifyAsync<TResult>(string url, T putObject, string? accessToken = null, CancellationToken token = default)
        {
            TResult? result = default;
            var json = _serializeService.SerializeJson(putObject);
            var putContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.PutAsync(url, putContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    result = _serializeService.DeserializeJson<TResult>(x.Result);
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException(content, null, response.StatusCode);
            }

            return result;

        }


        #endregion
        public void Dispose()
        {
            _client.Dispose();
            System.GC.Collect();
            System.GC.SuppressFinalize(this);
        }
    }
}
