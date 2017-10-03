using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UPCItemDb.Extensions;
using UPCItemDb.Requests;
using UPCItemDb.Responses;

namespace UPCItemDb
{
    public class UPCItemDBClient
    {
        private readonly FlurlClient _client;
        private readonly Url _apiBaseUrl;

        private readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private RateLimitResponse _rateLimitResponse;

        public ErrorResponse ErrorResponse { get; private set; }

        public UPCItemDBClient(UPCItemDBEnvironment env = UPCItemDBEnvironment.Trial, string userKey = "")
        {
            _client = new FlurlClient(cfg =>
            {
                cfg.BeforeCall = call =>
                {
                    call.Request.Headers.Add("key_type", "3scale");
                    if (string.IsNullOrEmpty(userKey)) return;
                    call.Request.Headers.Add("user_key", userKey);
                };
                cfg.OnError = call =>
                {
                    ErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(call.ErrorResponseBody);
                };

                cfg.AfterCall = call =>
                {
                    if (!call.Succeeded) return;

                    int.TryParse(call.Response.Headers.GetValues("x-ratelimit-remaining").First(), out int remaining);
                    int.TryParse(call.Response.Headers.GetValues("x-ratelimit-reset").First(), out int resetEpochSeconds);
                    int.TryParse(call.Response.Headers.GetValues("x-ratelimit-limit").First(), out int limit);

                    var current = 0;
                    if(call.Response.Headers.TryGetValues("x-ratelimit-current", out IEnumerable<string> currents))
                        int.TryParse(currents.First(), out current);

                    var reset = _epoch.AddSeconds(resetEpochSeconds);

                    _rateLimitResponse = new RateLimitResponse
                    {
                        Remaining = remaining,
                        Limit = limit,
                        Current = current,
                        Reset = reset
                    };
                };

                cfg.JsonSerializer = new NewtonsoftJsonSerializer(new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            });
            _apiBaseUrl = new Url("https://api.upcitemdb.com/prod/")
                .AppendPathSegment(env.ToBaseUrl());
        }

        public async Task<ItemsResponse> LookupByGetAsync(params string[] codes)
        {
            var items = await _apiBaseUrl
                .AppendPathSegment("lookup")
                .SetQueryParam("upc", string.Join(",", codes))
                .WithClient(_client)
                .GetJsonAsync<ItemsResponse>();

            return items.AssignRateLimit(_rateLimitResponse);
        } 

        public async Task<ItemsResponse> LookupByPostAsync(params string[] codes)
        {
            var items = await _apiBaseUrl
                .AppendPathSegment("lookup")
                .WithClient(_client)
                .PostJsonAsync(new {upc = string.Join(",", codes)})
                .ReceiveJson<ItemsResponse>();

            return items.AssignRateLimit(_rateLimitResponse);
        }

        public async Task<ItemsResponse> SearchByGetAsync(string keywords)
        {
            return await SearchByGetAsync(new SearchParameters(keywords));
        }

        public async Task<ItemsResponse> SearchByGetAsync(SearchParameters searchParameters)
        {
            var items = await _apiBaseUrl
                .AppendPathSegment("search")
                .SetQueryParam("s", searchParameters.Keywords)
                .SetQueryParam("brand", searchParameters.Brand)
                .SetQueryParam("category", searchParameters.Category)
                .SetQueryParam("offset", searchParameters.Offset)
                .SetQueryParam("match_mode", (int)searchParameters.MatchMode)
                .SetQueryParam("type", searchParameters.Type)
                .WithClient(_client)
                .GetJsonAsync<ItemsResponse>();

            return items.AssignRateLimit(_rateLimitResponse);
        }
        public async Task<ItemsResponse> SearchByPostAsync(string keywords)
        {
            return await SearchByPostAsync(new SearchParameters(keywords));
        }

        public async Task<ItemsResponse> SearchByPostAsync(SearchParameters searchParameters)
        {
            var items = await _apiBaseUrl
                .AppendPathSegment("search")
                .WithClient(_client)
                .PostJsonAsync(searchParameters)
                .ReceiveJson<ItemsResponse>();

            return items.AssignRateLimit(_rateLimitResponse);
        }
    }
}