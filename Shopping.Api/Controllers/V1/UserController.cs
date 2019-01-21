using System;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Models;
using Shopping.Api.Services.Interfaces;

namespace Shopping.Api.Controllers.V1
{

    [Route("v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<User> Get()
        {
            try
            {
                var user = _userService.GetUser();
                if (user == null)
                {
                    return NotFound();
                }
                return user;
            }
            catch (Exception ex)
            {
                //_logger.LogError("Internal server error", ex);
                return StatusCode(500);
            }
        }
    }
}
