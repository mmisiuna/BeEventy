namespace BeEventy.Data.Models
{
    public class Login
    {
            public record LoginRequest(string Login, string Password);
            public record LoginResponse(string Token, int UserId);
    }
}
