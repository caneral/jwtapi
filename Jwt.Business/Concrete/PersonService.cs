using Microsoft.EntityFrameworkCore;
using Jwt.Business.Abstract;
using Jwt.Business.Dtos.Person;
using Jwt.Business.Dtos.User;
using Jwt.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PBS.Business.Abstract;
using Jwt.DAL.Context;

namespace Jwt.Business.Concrete
{
    public class PersonService : IPersonService
    {
        private readonly JwtDbContext _dbContext;
        private readonly IAuthService _authService;
        private readonly IAppUserService _appUserService;

        private readonly Random random = new Random();

        // Kullanici adi, parola ve Originator kullanilarak bir sms paketi olusturulur.
        


        public PersonService(JwtDbContext dbContext, IAuthService authService, IAppUserService appUserService)
        {
            _dbContext = dbContext;
            _authService = authService;
            _appUserService = appUserService;
        }

        /// <summary>
        /// Yeni Personel Ekler.
        /// </summary>
        public async Task AddPersonAsync(PersonAddDto personAddDto)
        {
            

            var newPerson = new Person
            {
                TCNumber = personAddDto.TCNumber,
                Name = personAddDto.Name,
                Surname = personAddDto.Surname,
                PhoneNumber = personAddDto.PhoneNumber,
                Mail = personAddDto.Mail,
            };
            _dbContext.Persons.Add(newPerson);
            await _dbContext.SaveChangesAsync();


            var currentUser = await _dbContext.Persons.Where(p => !p.IsDeleted && p.TCNumber == personAddDto.TCNumber)
                .SingleOrDefaultAsync();
            var newAppUser = new UserRegisterDto
            {
                TCNumber = currentUser.TCNumber,
                FirstName = currentUser.Name,
                LastName = currentUser.Surname,
                Email = currentUser.Mail,
                Password = random.Next(99999, 1000000).ToString()
            };
            _authService.Register(newAppUser);


        }
        public Task<PersonGetDto> GetPersonByTCNumberAsync(string tcNumber)
        {
            return _dbContext.Persons
                .Where(p => !p.IsDeleted)
                .Select(p => new PersonGetDto
                {
                    Id = p.Id,
                    TCNumber = p.TCNumber,
                    Name = p.Name,
                    Surname = p.Surname,
                  
                    PhoneNumber = p.PhoneNumber,
                    Mail = p.Mail,
                    
                }).SingleOrDefaultAsync(p => p.TCNumber == tcNumber);
        }




    }
}
