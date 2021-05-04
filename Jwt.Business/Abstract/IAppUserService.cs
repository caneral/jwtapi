using AppCore.Entity.Identity;
using Jwt.Business.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Business.Abstract
{
    public interface IAppUserService
    {
        /// <summary>
        /// Kullanıcı ekleme
        /// </summary>
        Task<int> Add(AppUser user);

        /// <summary>
        ///TC numarasına göre kullanıcı getirme
        /// </summary>
        Task<AppUser> GetByTCNumber(string tcNumber);

        /// <summary>
        /// Kullanıcı güncelleme
        /// </summary>
        Task UpdateUser(int id, DateTime? refreshToken, string refreshTokenString);

        /// <summary>
        /// Kullanıcı id'ye göre rol getirme
        /// </summary>
        Task<List<AppClaim>> GetUserClaimsByUserId(int userId);

        /// <summary>
        /// Gelen refresh token ile kullanıcının refresh tokenini karşılaştırma
        /// </summary>
        Task<AppUser> ValidRefreshToken(string refreshToken);

        /// <summary>
        /// Kullanıcı güncelleme
        /// </summary>
        Task<int> UpdateAppUser(AppUserUpdateDto appUserUpdate);
        /// <summary>
        /// Person tablosundan silinen kişinin login kısmının ve appuserclaiminin silinmesi
        /// </summary>
        Task<int> DeleteAppUserAsync(string TCNumber);
    }
}
