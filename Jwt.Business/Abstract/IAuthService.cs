using AppCore.Entity.Identity;
using AppCore.Utilities.Security.Jwt;
using Jwt.Business.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Business.Abstract
{
    public interface IAuthService
    {
        /// <summary>
        /// Yeni kullanıcı ekleme
        /// </summary>
        AppUser Register(UserRegisterDto userRegisterDto);

        /// <summary>
        /// Kullanıcı Girişi
        /// </summary>
        Task<AppUser> Login(AppUserLoginDto userForLoginDto);

        /// <summary>
        /// Personel Id' ye göre Token oluşturma
        /// </summary>
        Task<AccessToken> CreateAccessToken(AppUser user, int personId);

        /// <summary>
        /// Gelen refresh token ile kullanıcının refresh tokenini karşılaştırma
        /// </summary>
        Task<AppUser> ValidRefreshToken(string refreshToken);
    }
}
