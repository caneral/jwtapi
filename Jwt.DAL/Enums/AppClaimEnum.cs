using System.ComponentModel.DataAnnotations;

namespace Jwt.DAL.Enums
{
    /// <summary>
    /// kullanıcı rolleri
    /// </summary>
    public enum AppClaimEnum
    {
        [Display(Name = "Yönetici")]
        Admin = 1,

        [Display(Name = "Kullanıcı")]
        User = 2
    }
}