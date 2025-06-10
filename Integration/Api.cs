using Integration.Client.Services;
using Newtonsoft.Json;
using Integration.Dtos.UserAccount;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

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
        readonly private JsonSerializerSettings jsonSerializerSettings = new()
        {
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
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

            // Set JsonConvert default settings
            JsonConvert.DefaultSettings = () => jsonSerializerSettings;

            // Get values from the config given their key and their target type.
            Settings? settings;

            using (StreamReader file = File.OpenText("appsettings.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject jObj = (JObject)JToken.ReadFrom(reader);
                settings = jObj.ToObject<Settings>();
            }

            _instance = this;
            if (settings != null)
                _baseUrl = settings.ApiUrl;

            _auth = new();
        }

        public async Task<T?> Get<T>(string path)
        {
            HttpResponseMessage httpResponse = await Client.GetAsync($"{BaseUrl}api/{path}");
            var data = JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());

            return data;
        }

        public async Task<TData?> Post<TBody, TData>(string path, TBody? body)
        {
            // Manually serialize the body to JSON
            string jsonBody = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // Send the POST request
            HttpResponseMessage httpResponse = await Client.PostAsync($"{BaseUrl}api/{path}", content);

            // Read the response and deserialize it
            string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            TData? data = JsonConvert.DeserializeObject<TData>(jsonResponse);

            return data;
        }
    };
}
