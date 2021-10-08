using BlogSystem.DTO;
using BlogSystem.Models;
using BlogSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        private IAuthentication _authentication;

        public AuthenticationController(UserManager<ApplicationUser> userManager, IAuthentication authentication)
        {
            this.userManager = userManager;
            _authentication = authentication;
        }

        // sign up vistor 

        [HttpPost]
        [Route("register-vistor")]

        public async Task<IActionResult> RegisterVistor([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authentication.RegisterVistorAsync(model);
                if (!result.IsAuthenticated)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        // sign up Moderator 

        [HttpPost]
        [Route("register-moderator")]

        public async Task<IActionResult> RegisterModerator([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authentication.RegisterModeratorAsync(model);
                if (!result.IsAuthenticated)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }


        [HttpPost]
        [Route("register-admin")]

        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authentication.RegisterAdminAsync(model);
                if (!result.IsAuthenticated)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }


        // login


        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authentication.LoginAsync(model);
                if (!result.IsAuthenticated)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }





    }
}
