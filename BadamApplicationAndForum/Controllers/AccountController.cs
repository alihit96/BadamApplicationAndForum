using BadamApplicationAndForum.Data.Dtos;
using BadamApplicationAndForum.Data.Dtos.Alarms;
using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BadamApplicationAndForum.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IApplicationUser _applicationUser;
        private readonly IAlarm _alarm;
        public AccountController(IApplicationUser applicationUser, IAlarm alarm)
        {
            _applicationUser = applicationUser;
            _alarm = alarm;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/app/register")]
        public IActionResult Register(RegisterDto register)
        {
            if (_applicationUser.ExistsUser(register.UserName))
            {
                return Ok(new
                {
                    Result = "کاربر با این مشخصات وجود دارد"
                });
            }
            var user =_applicationUser.CreateUser(new ApplicationUser
            {
                FullName = register.FullName,
                UserName = register.UserName,
                Password = register.Password,
                Email = register.Email,
                UserType = register.UserType,
            });



            return Ok(new
            {
                Result = "کاربر با موفقیت ثبت شد"
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/app/login")]
        public IActionResult Login(LoginDto login)
        {
            var response = _applicationUser.AuthenticateUser(login);
            if (response == null)
                return BadRequest(new { message = "نام کاربری یا رمز عبور اشتباه است" });
            return Ok(response);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        [Route("api/app/useralarms/{id}")]
        public ActionResult UserAlarms(string Id)
        {
            var user = _applicationUser.FindUserById(Id);
            var alarms = _alarm.GetAlarmsById(user.Id.ToString());
            return Ok(new AlarmsIndexingDto()
            {
                Alarms = alarms
            });
        }

        //[HttpGet]
        //[Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        //[Route("api/app/userInfo/{id}")]
        //public ActionResult UserInfo(string Id)
        //{
        //    var user = _applicationUser.FindUserById(Id);
        //    var dms = user.DirectMessages;
        //    return Ok(new 
        //    { 
        //        Messages = dms,
        //        UserName = user.UserName,
        //        FullName = user.FullName,
        //        UserType = user.UserType,
        //        Email = user.Email,
        //    });
        //}
    }
}
