using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AppCore.Utilities.Security.Encryption
{
    /// <summary>
    /// Security key oluşturulur
    /// </summary>
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
