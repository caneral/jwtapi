using System;

namespace Jwt.Business.Dtos.Person
{
    /// <summary>
    /// Personel Ekleme Dto
    /// </summary>
    public class PersonAddDto
    {
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

       
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Personel Mail Adresi
        /// </summary>
        public string Mail { get; set; }

      
    }
}
