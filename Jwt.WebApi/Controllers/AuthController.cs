using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Jwt.Business.Abstract;
using Jwt.Business.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PBS.Business.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Jwt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IPersonService _personService;


        public AuthController(IAuthService authService, IPersonService personService)
        {
            _authService = authService;
            _personService = personService;
        }

        //[HttpPost("AddUser")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<string> AddPersonAsync(UserRegisterDto userRegisterDto)
        //{
        //    try
        //    {
        //        var result = _authService.Register(userRegisterDto);
        //        if (result != null)
        //        {

        //            return StatusCode(StatusCodes.Status201Created);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }

        //    return BadRequest();
        //}

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Login(AppUserLoginDto appUserLoginDto)
        {
            try
            {
                var appUser = await _authService.Login(appUserLoginDto);
                if (appUser == null)
                {
                    return Unauthorized("Kullanıcı adı veya şifre geçerli değil.");
                }

                var person = await _personService.GetPersonByTCNumberAsync(appUser.TCNumber);
                var accessToken = await _authService.CreateAccessToken(appUser, person.Id);
                return Ok(accessToken);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("CreateTokenWithRefreshToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> RefreshTokenLogin(string refreshToken)
        {
            var user = await _authService.ValidRefreshToken(refreshToken);
            if (user.RefreshTokenEndDate < DateTime.Now)
            {
                return Unauthorized();
            }
            try
            {
                var person = await _personService.GetPersonByTCNumberAsync(user.TCNumber);
                var accessToken = await _authService.CreateAccessToken(user, person.Id);
                return Ok(accessToken);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex.Message);
            }
        }
    }
}
