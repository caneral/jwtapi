using AppCore.Entity.Identity;
using Jwt.DAL.Context;
using System.Linq;
using System.Threading.Tasks;
using Jwt.Business.Abstract;

namespace Jwt.Business.Concrete
{
    public class AppClaimService : IAppClaimService
    {
        private readonly JwtDbContext _dbContext;

        public AppClaimService(JwtDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Add(AppUser user)
        {
            var currentUser = _dbContext.AppUsers.FirstOrDefault(p => p.Id == user.Id);
            var userClaim = _dbContext.AppUsers.Add(user);
            return await _dbContext.SaveChangesAsync();
        }

        public AppClaim GetClaim(string claimName)
        {
            return _dbContext.AppClaims.SingleOrDefault(c => c.Name == claimName);
        }

    }
}
