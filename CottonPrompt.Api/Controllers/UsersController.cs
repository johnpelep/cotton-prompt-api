﻿using CottonPrompt.Api.Messages.Users;
using CottonPrompt.Infrastructure.Models;
using CottonPrompt.Infrastructure.Models.Users;
using CottonPrompt.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Net;

namespace CottonPrompt.Api.Controllers
{
    [Authorize]
    [RequiredScope("access_as_user")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet("login")]
        [ProducesResponseType<GetUsersModel>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoginAsync()
        {
            var user = User;

            var idClaim = user.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier");
            var nameClaim = user.FindFirst("name");
            var emailClaim = user.FindFirst("preferred_username");

            if (idClaim is null || nameClaim is null || emailClaim is null)
            {
                return BadRequest("Missing required user claims");
            }

            var id = Guid.Parse(idClaim.Value);
            var name = nameClaim.Value;
            var email = emailClaim.Value;
            var result = await userService.LoginAsync(id, name, email);
            return Ok(result);
        }

        [HttpGet("unregistered")]
        [ProducesResponseType<IEnumerable<GetUsersModel>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUnregisteredAsync()
        {
            var result = await userService.GetUnregisteredAsync();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("registered")]
        [ProducesResponseType<IEnumerable<GetUsersModel>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRegisteredAsync()
        {
            var result = await userService.GetRegisteredAsync();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}/can-update-role")]
        [ProducesResponseType<CanDoModel>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CanUpdateRoleAsync([FromRoute] Guid id, [FromQuery] string role)
        {
            var result = await userService.CanUpdateRoleAsync(id, role);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPut("{id}/role")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateRoleAsync([FromRoute] Guid id, [FromBody] UpdateUserRoleRequest request)
        {
            await userService.UpdateRoleAsync(id, request.Role, request.UpdatedBy);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> AddAsync([FromBody] AddUserRequest request)
        {
            await userService.AddAsync(request.Id, request.Name, request.Email, request.Role, request.CreatedBy);
            return NoContent();
        }
    }
}
    