using System;

namespace AppCore.Entity
{
    /// <summary>
    /// DB Kayıt bilgileri
    /// </summary>
    public abstract class Audit
    {
        /// <summary>
        /// Oluşturulma Tarihi
        /// </summary>
        public DateTime CDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Değiştirilme Tarihi
        /// </summary>
        public DateTime? MDate { get; set; }

        /// <summary>
        /// Değiştiren Kullanıcı
        /// </summary>
        public string MUser { get; set; }


        /// <summary>
        /// Oluşturan Kullanıcı
        /// </summary>
        public string CUser { get; set; }
    }
}
