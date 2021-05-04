using AppCore.Entity;
using System;
using System.Collections.Generic;

namespace Jwt.DAL.Entities
{
    /// <summary>
    /// Personel Bilgileri
    /// </summary>
    public class Person : Audit, IEntity, ISoftDeleted
    {
        /// <summary>
        /// Personel Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Personel TC Kimlik Numarası
        /// </summary>
        public string TCNumber { get; set; }

        /// <summary>
        /// Personel Ad
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Personel Soyad
        /// </summary>
        public string Surname { get; set; }


        /// <summary>
        /// Personel Telefon Numarası
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Personel Mail Adresi
        /// </summary>
        public string Mail { get; set; }


        public bool IsDeleted { get; set; }
    }
}
