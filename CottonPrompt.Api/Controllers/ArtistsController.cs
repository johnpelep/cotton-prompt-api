﻿using CottonPrompt.Infrastructure.Models;
using CottonPrompt.Infrastructure.Services.Artists;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CottonPrompt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController(IArtistService artistService) : ControllerBase
    {
        [HttpGet("{id}/can-claim")]
        [ProducesResponseType<IEnumerable<CanDoModel>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CanClaimAsnyc([FromRoute] Guid id)
        {
            var result = await artistService.CanClaimAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}/can-claim-change-request")]
        [ProducesResponseType<IEnumerable<CanDoModel>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CanClaimChangeRequestAsnyc([FromRoute] Guid id)
        {
            var result = await artistService.CanClaimChangeRequestAsync(id);
            return Ok(result);
        }
    }
}
    