namespace LinuxLudo.API.Domain.Resources.Response
{
    public class TokenResponse
    {
        public string Token { get; }

        public TokenResponse(string token)
        {
            Token = token;
        }
    }
}