using AppCore.Entity.Identity;
using Microsoft.EntityFrameworkCore;
using Jwt.Business.Abstract;
using Jwt.Business.Dtos.User;
using Jwt.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBS.Business.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly JwtDbContext _dbContext;
        public AppUserService(JwtDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> Add(AppUser user)
        {
            _dbContext.Add(user);
            return _dbContext.SaveChangesAsync();
        }

        public async Task<AppUser> GetByTCNumber(string tcNumber)
        {
            return await _dbContext.AppUsers.SingleOrDefaultAsync(u => u.TCNumber == tcNumber);
        }

        public async Task<List<AppClaim>> GetUserClaimsByUserId(int userId)
        {
            return await _dbContext.AppUserClaims.Include(q => q.AppClaimFK)
                .Where(u => u.AppUserId == userId).Select(q => q.AppClaimFK).ToListAsync();
        }

        public async Task<int> UpdateAppUser(AppUserUpdateDto appUserUpdate)
        {
            var currentAppUser = await _dbContext.AppUsers.Where(p => p.TCNumber == appUserUpdate.TCNumber).SingleOrDefaultAsync();
            currentAppUser.FirstName = appUserUpdate.FirstName;
            currentAppUser.LastName = appUserUpdate.LastName;
            currentAppUser.Email = appUserUpdate.Email;
            _dbContext.AppUsers.Update(currentAppUser);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUser(int id, DateTime? refreshToken, string refreshTokenString)
        {
            var currentPerson = await _dbContext.AppUsers.FirstOrDefaultAsync(p => p.Id == id);
            if (currentPerson != null)
            {
                currentPerson.RefreshTokenEndDate = refreshToken;
                currentPerson.RefreshToken = refreshTokenString;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<AppUser> ValidRefreshToken(string refreshToken)
        {
            return await _dbContext.AppUsers.FirstOrDefaultAsync(p => p.RefreshToken == refreshToken);
        }
        public async Task<int> DeleteAppUserAsync(string TCNumber)
        {
            var currentAppUser = await _dbContext.AppUsers.FirstOrDefaultAsync(p => p.TCNumber == TCNumber);
            var currentAppUserClaim = await _dbContext.AppUserClaims.FirstOrDefaultAsync(p => p.AppUserId == currentAppUser.Id);

            if (currentAppUser == null || currentAppUserClaim == null)
            {
                throw new Exception($"{TCNumber} tc nolu Personele ait bilgi bulunamadı.");
            }
            currentAppUser.IsDeleted = true;
            currentAppUser.MDate = DateTime.Now;

            currentAppUserClaim.IsDeleted = true;
            currentAppUserClaim.MDate = DateTime.Now;

            return await _dbContext.SaveChangesAsync();
        }
    }
}
