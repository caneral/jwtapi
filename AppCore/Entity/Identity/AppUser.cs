using System;
using System.Collections.Generic;

namespace AppCore.Entity.Identity
{
    public class AppUser : Audit, IEntity, ISoftDeleted
    {

        public AppUser()
        {
            AppUserClaims = new List<AppUserClaim>();
        }

        public int Id { get; set; }

        public string TCNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public byte[] PasswordSalt { get; set; }

        public byte[] PasswordHash { get; set; }

        public ICollection<AppUserClaim> AppUserClaims { get; set; }

        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }

        public bool IsDeleted { get; set; }

    }
}
