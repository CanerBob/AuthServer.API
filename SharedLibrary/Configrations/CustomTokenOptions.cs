﻿namespace SharedLibrary.Configrations;
public class CustomTokenOptions
{
    public List<string> Audience { get; set; }
    public string Issuer{ get; set; }
    public int AccessTokenExpiration{ get; set; }
    public int RefreshTokenExpiration{ get; set; }
    public string SecurityKey{ get; set; }
}
