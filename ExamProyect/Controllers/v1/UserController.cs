using Authentication;
using AutoMapper;
using Domain.Model;
using ExamProyect.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;
        private readonly IJWTServices _iJWTService;
        public UserController(IUser iUser, IMapper mapper, IJWTServices jWTServices)
        {
            _IUser = iUser;
            _mapper = mapper;
            _iJWTService = jWTServices;
        }
        [HttpGet(nameof(GetUserById))]
        public IActionResult GetUserById(int id)
        {
            var result = _IUser.GetUser(id);
            var userMap = _mapper.Map<UserModel>(result);
            if (result is not null)
            {
                return Ok(userMap);
            }
            return BadRequest("No record Found");
        }
       
        [HttpGet(nameof(GetAllUser))]
        public IActionResult GetAllUser()
        {
            var result = _IUser.GetAllUsers();
            var userMap = _mapper.Map<List<UserModel>>(result);
            if(result.Count() > 0)
            {
                return Ok(userMap);
            }

            return BadRequest("No record loaded");
        }
        [AllowAnonymous]
        [HttpPost(nameof(InsertUser))]
        public ActionResult InsertUser(EditUserModel user)
        {

            var userMap = _mapper.Map<User>(user);
            
            var result = _IUser.InsertUser(userMap);
            if (result)
            {
                return Ok("Created!");
            }

            return BadRequest("No Created");
        }

        [HttpPut(nameof(UpdateUser))]
        public ActionResult UpdateUser(UserModel user)
        {
            var userMap = _mapper.Map<User>(user);
            var result = _IUser.UpdateUser(userMap);
            if (result)
            {
                return Ok("Updated");
            }

            return BadRequest("No Updated");
        }
        [HttpDelete]
        public ActionResult DeleteUser(UserModel user)
        {
            var userMap = _mapper.Map<User>(user);
            var result = _IUser.DeleteUser(userMap);
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
