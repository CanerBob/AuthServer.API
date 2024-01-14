using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SharedLibrary.Services;
public class SignService
{
    public static SecurityKey GetSymetricSecurityKey(string securityKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }
}
