using Integration.Dtos.UserAccount;
using System.Net.Http;

namespace Integration.Client.Services
{
    public class AuthService
    {
        public AuthService() { }

        private AuthInfo? _authInfo;
        public AuthInfo? Info { get => _authInfo; }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var response = await Api.Instance.Post<LoginRequest, LoginResponse>("User/Login", loginRequest);
            if (response == null)
                throw new Exception("Login Failed for unknown reasons");

            _authInfo = response;
            return response;
        }
    
        public async Task<UserAccountFlatResponse> GetUserInfo()
        {
            var response = await Api.Instance.Get<UserAccountFlatResponse>("User/UserInfo");
            if (response == null)
                throw new Exception("Login Failed for unknown reasons");

            return response;
        }
    }
}
