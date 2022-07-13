using Authentication;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Model;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProyect.Controllers.v1
{
    [Authorize]
    [Route("v1/api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser _IUser;
        private readonly IJWTServices _iJWTService;
        public UserController(IUser iUser, IMapper mapper, IJWTServices jWTServices)
        {
            _IUser = iUser;
            _iJWTService = jWTServices;
        }
        [HttpGet(nameof(GetUserById))]
        public IActionResult GetUserById(int id)
        {
            var result = _IUser.GetUser(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No record Found");
        }
       
        [HttpGet(nameof(GetAllUser))]
        public IActionResult GetAllUser()
        {
            var result = _IUser.GetAllUsers();
            if(result.Count() > 0)
            {
                return Ok(result);
            }

            return BadRequest("No record loaded");
        }
        [AllowAnonymous]
        [HttpPost(nameof(InsertUser))]
        public ActionResult InsertUser(EditUserModel user)
        {
            
            var result = _IUser.InsertUser(user);
            if (result)
            {
                return Ok("Created!");
            }

            return BadRequest("No Created");
        }

        [HttpPut(nameof(UpdateUser))]
        public ActionResult UpdateUser(EditUserModel user)
        {
            var result = _IUser.UpdateUser(user);
            if (result)
            {
                return Ok("Updated");
            }

            return BadRequest("No Updated");
        }
        [HttpDelete]
        public ActionResult DeleteUser(UserModel user)
        {
            var result = _IUser.DeleteUser(user);
            if (result)
            {
                return Ok("Deleted");
            }

            return BadRequest("No Deleted");
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public IActionResult Login(string user, string password)
        {
            var result = _IUser.IsValidUser(user, password);
            if (result)
            {
                var token = _iJWTService.Authenticate(user);
                return Ok(token);
            }

            return Unauthorized();
        }
    }
}
