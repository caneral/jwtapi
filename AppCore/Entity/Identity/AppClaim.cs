using System.Collections.Generic;

namespace AppCore.Entity.Identity
{
    public class AppClaim : Audit, IEntity, ISoftDeleted
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<AppUserClaim> AppUserClaims { get; set; }
        public bool IsDeleted { get; set; }
    }
}
