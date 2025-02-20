using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MOFA.StockManagement.ApplicationService.AppService.Extension
{
    public interface IHttpClientHelper<T> : IDisposable
    {
        Task<T?> FindAsync(string url, string? accessToken = null, CancellationToken token = default);
        Task<TResult?> FindAsync<TResult>(string url, string? accessToken = null, CancellationToken token = default);

        Task<TResult?> FindAsync<TResult>(string url,
            T postObject,
            string? accessToken = null,
            CancellationToken token = default);
        Task<TResult?> FindAsync<TResult, TFactors>(string url,
            TFactors postObject,
            string? accessToken = null,
            CancellationToken token = default);

        Task<IList<T>?> SelectAsync(string url, string? accessToken = null, CancellationToken token = default);

        Task<IList<TResult>?> SelectAsync<TResult>(string url,
            T postObject,
            string? accessToken = null,
            CancellationToken token = default) where TResult : class;

        Task<T?> AddAsync(string url, T postObject, string? accessToken = null, CancellationToken token = default);

        Task<TResult?> AddAsync<TResult>(string url, T postObject, string? accessToken = null,
            CancellationToken token = default);

        Task ModifyAsync(string url, T putObject, string? accessToken = null, CancellationToken token = default);
        Task<TResult?> ModifyAsync<TResult>(string url, T putObject, string? accessToken = null,
            CancellationToken token = default);

        Task DeleteAsync(string url, string? accessToken = null, CancellationToken token = default);
    }

    public class HttpClientServiceHelper<T> : IHttpClientHelper<T>
    {
        private readonly HttpClient _client;

        public HttpClientServiceHelper(IHttpClientFactory httpClient)
        {
            _client = httpClient.CreateClient("Service");
        }

        #region FindAsync

        public async Task<TResult?> FindAsync<TResult>(string url, T postObject, string? accessToken = null, CancellationToken token = default)
        {
            var result = default(TResult);
            var json = JsonSerializer.Serialize(postObject);
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.PostAsync(url, postContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {

                    if (typeof(TResult).Namespace != "System")
                    {
                        result = JsonSerializer.Deserialize<TResult>(x.Result, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    }
                    else
                    {
                        result = (TResult?)Convert.ChangeType(x?.Result, typeof(TResult));
                    }
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }

        public async Task<T?> FindAsync(string url, string? accessToken = null, CancellationToken token = default)
        {
            var result = default(T);

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.GetAsync(url, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return result;
                }

                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    if (typeof(T).Namespace != "System")
                    {
                        result = JsonSerializer.Deserialize<T>(x.Result, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    }
                    else
                    {
                        result = (T?)Convert.ChangeType(x?.Result, typeof(T));
                    }
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }
        public async Task<TResult?> FindAsync<TResult>(string url, string? accessToken = null, CancellationToken token = default)
        {
            var result = default(TResult);

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.GetAsync(url, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return result;
                }

                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    if (typeof(TResult).Namespace != "System")
                    {
                        result = JsonSerializer.Deserialize<TResult>(x.Result, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    }
                    else
                    {
                        result = (TResult?)Convert.ChangeType(x?.Result, typeof(TResult));
                    }
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }
        public async Task<TResult?> FindAsync<TResult, TFactors>(string url, TFactors postObject, string? accessToken = null, CancellationToken token = default)
        {
            var result = default(TResult);
            var json = JsonSerializer.Serialize(postObject);
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.PostAsync(url, postContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {

                    if (typeof(TResult).Namespace != "System")
                    {
                        result = JsonSerializer.Deserialize<TResult>(x.Result, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    }
                    else
                    {
                        result = (TResult?)Convert.ChangeType(x?.Result, typeof(TResult));
                    }
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }

        #endregion

        #region SelectAsync

        public async Task<IList<T>?> SelectAsync(string url, string? accessToken = null, CancellationToken token = default)
        {
            IList<T>? result = null;

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.GetAsync(url, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    result = JsonSerializer.Deserialize<IList<T>>(x.Result, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;

        }

        public async Task<IList<TResult>?> SelectAsync<TResult>(string url, T postObject, string? accessToken = null, CancellationToken token = default) where TResult : class
        {
            IList<TResult>? result = null;

            var json = JsonSerializer.Serialize(postObject);
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.PostAsync(url, postContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    result = JsonSerializer.Deserialize<IList<TResult>>(x.Result, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }

        #endregion

        #region DeleteAysnc

        public async Task DeleteAsync(string url, string? accessToken = null, CancellationToken token = default)
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.DeleteAsync(url, token).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
        }


        #endregion

        #region AddAysnc

        public async Task<T?> AddAsync(string url, T postObject, string? accessToken = null, CancellationToken token = default)
        {
            T? result = default;

            var json = JsonSerializer.Serialize(postObject);
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.PostAsync(url, postContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    result = JsonSerializer.Deserialize<T>(x.Result, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }

        public async Task<TResult?> AddAsync<TResult>(string url, T postObject, string? accessToken = null, CancellationToken token = default)
        {
            TResult? result = default;

            var json = JsonSerializer.Serialize(postObject, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.PostAsync(url, postContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    result = JsonSerializer.Deserialize<TResult>(x.Result, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }

        #endregion

        #region ModifyAsync

        public async Task ModifyAsync(string url, T putObject, string? accessToken = null, CancellationToken token = default)
        {
            var json = JsonSerializer.Serialize(putObject);
            var putContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.PutAsync(url, putContent, token).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
        }
        public async Task<TResult?> ModifyAsync<TResult>(string url, T putObject, string? accessToken = null, CancellationToken token = default)
        {
            TResult? result = default;
            var json = JsonSerializer.Serialize(putObject);
            var putContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.PutAsync(url, putContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                if (typeof(TResult) == typeof(string))
                {
                    return (TResult)Convert.ChangeType(await response.Content.ReadAsStringAsync(token), typeof(TResult));
                }
                else
                {
                    await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                    {
                        result = JsonSerializer.Deserialize<TResult>(x.Result, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    }, token);
                }
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;

        }

        #endregion


        public void Dispose()
        {
            _client.Dispose();
            System.GC.Collect();
        }
    }
    public class HttpClientIdentityServiceHelper<T> : IHttpClientHelper<T>
    {
        private readonly HttpClient _client;

        public HttpClientIdentityServiceHelper(IHttpClientFactory httpClient)
        {
            _client = httpClient.CreateClient("IdService");
        }

        #region FindAsync

        public async Task<TResult?> FindAsync<TResult>(string url, T postObject, string? accessToken = null, CancellationToken token = default)
        {
            var result = default(TResult);
            var json = JsonSerializer.Serialize(postObject);
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.PostAsync(url, postContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {

                    if (typeof(TResult).Namespace != "System")
                    {
                        result = JsonSerializer.Deserialize<TResult>(x.Result, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    }
                    else
                    {
                        result = (TResult?)Convert.ChangeType(x?.Result, typeof(TResult));
                    }
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }

        public async Task<T?> FindAsync(string url, string? accessToken = null, CancellationToken token = default)
        {
            var result = default(T);

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.GetAsync(url, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return result;
                }

                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    if (typeof(T).Namespace != "System")
                    {
                        result = JsonSerializer.Deserialize<T>(x.Result, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    }
                    else
                    {
                        result = (T?)Convert.ChangeType(x?.Result, typeof(T));
                    }
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }
        public async Task<TResult?> FindAsync<TResult>(string url, string? accessToken = null, CancellationToken token = default)
        {
            var result = default(TResult);

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.GetAsync(url, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return result;
                }

                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    if (typeof(TResult).Namespace != "System")
                    {
                        result = JsonSerializer.Deserialize<TResult>(x.Result, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    }
                    else
                    {
                        result = (TResult?)Convert.ChangeType(x?.Result, typeof(TResult));
                    }
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }
        public async Task<TResult?> FindAsync<TResult, TFactors>(string url, TFactors postObject, string? accessToken = null, CancellationToken token = default)
        {
            var result = default(TResult);
            var json = JsonSerializer.Serialize(postObject);
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.PostAsync(url, postContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {

                    if (typeof(TResult).Namespace != "System")
                    {
                        result = JsonSerializer.Deserialize<TResult>(x.Result, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    }
                    else
                    {
                        result = (TResult?)Convert.ChangeType(x?.Result, typeof(TResult));
                    }
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }
        #endregion

        #region SelectAsync

        public async Task<IList<T>?> SelectAsync(string url, string? accessToken = null, CancellationToken token = default)
        {
            IList<T>? result = null;

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.GetAsync(url, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    result = JsonSerializer.Deserialize<IList<T>>(x.Result, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;

        }

        public async Task<IList<TResult>?> SelectAsync<TResult>(string url, T postObject, string? accessToken = null, CancellationToken token = default) where TResult : class
        {
            IList<TResult>? result = null;

            var json = JsonSerializer.Serialize(postObject);
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.PostAsync(url, postContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    result = JsonSerializer.Deserialize<IList<TResult>>(x.Result, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }

        #endregion

        #region DeleteAysnc

        public async Task DeleteAsync(string url, string? accessToken = null, CancellationToken token = default)
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.DeleteAsync(url, token).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
        }


        #endregion

        #region AddAysnc

        public async Task<T?> AddAsync(string url, T postObject, string? accessToken = null, CancellationToken token = default)
        {
            T? result = default;

            var json = JsonSerializer.Serialize(postObject);
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.PostAsync(url, postContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    result = JsonSerializer.Deserialize<T>(x.Result, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }

        public async Task<TResult?> AddAsync<TResult>(string url, T postObject, string? accessToken = null, CancellationToken token = default)
        {
            TResult? result = default;

            var json = JsonSerializer.Serialize(postObject, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
            var postContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.PostAsync(url, postContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                {
                    result = JsonSerializer.Deserialize<TResult>(x.Result, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }, token);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }

        #endregion

        #region ModifyAsync

        public async Task ModifyAsync(string url, T putObject, string? accessToken = null, CancellationToken token = default)
        {
            var json = JsonSerializer.Serialize(putObject);
            var putContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.PutAsync(url, putContent, token).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
        }
        public async Task<TResult?> ModifyAsync<TResult>(string url, T putObject, string? accessToken = null, CancellationToken token = default)
        {
            TResult? result = default;
            var json = JsonSerializer.Serialize(putObject);
            var putContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _client.PutAsync(url, putContent, token).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                if (typeof(TResult) == typeof(string))
                {
                    return (TResult)Convert.ChangeType(await response.Content.ReadAsStringAsync(token), typeof(TResult));
                }
                else
                {
                    await response.Content.ReadAsStringAsync(token).ContinueWith(x =>
                    {
                        result = JsonSerializer.Deserialize<TResult>(x.Result, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    }, token);
                }
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync(token);
                response.Content.Dispose();

                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;

        }

        #endregion


        public void Dispose()
        {
            _client.Dispose();
            System.GC.Collect();
        }
    }
}
