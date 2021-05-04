using AppCore.Entity.Identity;
using System.Threading.Tasks;

namespace Jwt.Business.Abstract
{
    public interface IAppClaimService
    {
        /// <summary>
        /// Kullanıcıya rol ekleme
        /// </summary>
        Task<int> Add(AppUser user);

        /// <summary>
        /// Kullanıcıya ait rolleri getirme
        /// </summary>
        AppClaim GetClaim(string claimName);
    }
}
