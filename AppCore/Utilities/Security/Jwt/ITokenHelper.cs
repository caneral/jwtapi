using AppCore.Entity.Identity;
using System.Collections.Generic;

namespace AppCore.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(AppUser user, List<AppClaim> appClaims, int personId);
    }
}
