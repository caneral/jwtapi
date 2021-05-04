namespace AppCore.Entity.Identity
{
    public class AppUserClaim : Audit, IEntity, ISoftDeleted
    {
        public int Id { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUserFK { get; set; }

        public int AppClaimId { get; set; }
        public AppClaim AppClaimFK { get; set; }

        public bool IsDeleted { get; set; }

    }
}
