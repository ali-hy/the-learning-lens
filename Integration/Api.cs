using Integration.Client.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Integration.Dtos.UserAccount;

namespace Integration
{
    public class Api
    {
        readonly HttpClient Client;
        private static Api? _instance;
        public static Api Instance
        {
            get => _instance ?? new();
        }

        // JsonSerializer
        private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        // Settings
        private string? _baseUrl;
        public string BaseUrl { get => _baseUrl ?? "https://localhost:7254/"; }

        // Authentication
        private AuthService _auth;
        public AuthService Auth { get => _auth; }

        public static void Initialize()
        {
            _instance ??= new();
        }

        public async static Task<Api> Initialize(LoginRequest? loginRequest)
        {
            _instance ??= new();
            if (loginRequest != null)
            {
                await _instance.Auth.Login(loginRequest);
                _instance.Client.DefaultRequestHeaders.Authorization = new("Bearer", _instance.Auth.Info?.AccessToken);
            }
            return _instance;
        }

        Api()
        {
            // Initialize HttpClient
            Client = new();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            Client.DefaultRequestHeaders.Add("User-Agent", ".NET Tester");

            // Load Configuration
            // Build a config object, using env vars and JSON providers.
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            // Get values from the config given their key and their target type.
            Settings? settings = config.Get<Settings>();

            _instance = this;
            if (settings != null)
                _baseUrl = settings.ApiUrl;

            _auth = new();
        }

        public async Task<T?> Get<T>(string path)
        {
            HttpResponseMessage httpResponse = await Client.GetAsync($"{BaseUrl}api/{path}");
            var data = await JsonSerializer.DeserializeAsync<T>(httpResponse.Content.ReadAsStream(), _jsonSerializerOptions);

            return data;
        }

        public async Task<TData?> Post<TBody, TData>(string path, TBody? body)
        {
            HttpResponseMessage httpResponse = await Client.PostAsJsonAsync($"{BaseUrl}api/{path}", body);
            var data = await JsonSerializer.DeserializeAsync<TData>(httpResponse.Content.ReadAsStream(), _jsonSerializerOptions);

            return data;
        }
    };
}
