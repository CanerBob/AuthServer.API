namespace AuthServer.Core.DTOs;
public class TokenDTO
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpitaon { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpration { get; set; }
}
