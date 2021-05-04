using AppCore.Entity.Identity;
using AppCore.Utilities.Security.Hashing;
using AppCore.Utilities.Security.Jwt;
using Jwt.Business.Abstract;
using Jwt.Business.Dtos.User;
using Jwt.DAL.Enums;
using System.Threading.Tasks;

namespace Jwt.Business.Concrete
{
    public class AuthService : IAuthService
    {
        public ITokenHelper _tokenHelper;
        private readonly IAppUserService _appUserService;
        private readonly IAppClaimService _appClaimService;

        public AuthService(ITokenHelper tokenHelper, IAppUserService appUserService,
            IAppClaimService appClaimService)
        {
            _tokenHelper = tokenHelper;
            _appUserService = appUserService;
            _appClaimService = appClaimService;
        }

        public AppUser Register(UserRegisterDto userRegisterDto)
        {
            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash,
                out byte[] passwordSalt);
            var newUser = new AppUser
            {
                TCNumber = userRegisterDto.TCNumber,
                Email = userRegisterDto.Email,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
            };

            //var claimUser = _appClaimService.GetClaim(AppClaimEnum.User.ToString());
            var claimAdmin = _appClaimService.GetClaim(AppClaimEnum.Admin.ToString());
            newUser.AppUserClaims.Add(new AppUserClaim
            {
                AppClaimId = claimAdmin.Id
            });
            //newUser.AppUserClaims.Add(new AppUserClaim
            //{
            //    AppClaimId = claimAdmin.Id
            //});
            _appUserService.Add(newUser);

            return newUser;
        }

        public async Task<AppUser> Login(AppUserLoginDto appUserLoginDto)
        {
            var appUser = await _appUserService.GetByTCNumber(appUserLoginDto.TCNumber);
            if (appUser == null)
            {
                return null;
            }

            if (!HashingHelper.VerifyPasswordHash(appUserLoginDto.Password, appUser.PasswordHash,
               appUser.PasswordSalt))
            {
                return null;
            }

            return appUser;
        }

        public async Task<AccessToken> CreateAccessToken(AppUser user, int personId)
        {
            var userClaimList = await _appUserService.GetUserClaimsByUserId(user.Id);
            var accessToken = _tokenHelper.CreateToken(user, userClaimList, personId);
            await _appUserService.UpdateUser(user.Id, accessToken.RefreshTokenEndDate, accessToken.RefreshToken);
            return accessToken;
        }

        public async Task<AppUser> ValidRefreshToken(string refreshToken)
        {
            var currentUser = await _appUserService.ValidRefreshToken(refreshToken);
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
    }
}

