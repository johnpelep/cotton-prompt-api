﻿using Microsoft.AspNetCore.Mvc;

namespace CottonPrompt.Api.Messages.DesignBrackets
{
    public class GetDesignBracketsRequest
    {
        [FromQuery(Name = "hasActiveFilter")]
        public bool HasActiveFilter { get; set; }

        [FromQuery(Name = "active")]
        public bool Active { get; set; }
    }
}
