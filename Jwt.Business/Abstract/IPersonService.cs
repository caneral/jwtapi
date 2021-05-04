using Jwt.Business.Dtos.Person;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PBS.Business.Abstract
{
    /// <summary>
    /// Personel Servis
    /// </summary>
    public interface IPersonService
    {

        /// <summary>
        /// Yeni personel ekleme
        /// </summary>
        public Task AddPersonAsync(PersonAddDto personAddDto);
        public Task<PersonGetDto> GetPersonByTCNumberAsync(string tcNumber);



    }
}
