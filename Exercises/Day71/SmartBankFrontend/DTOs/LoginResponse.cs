namespace SmartBankFrontend.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
