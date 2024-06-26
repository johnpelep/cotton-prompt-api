﻿using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.EmailTemplates;

namespace CottonPrompt.Infrastructure.Extensions
{
    internal static class EmailTemplatesExtensions
    {
        internal static EmailTemplatesModel AsModel(this EmailTemplate entity)
        {
            var result = new EmailTemplatesModel(entity.OrderProofReadyEmail);
            return result;
        }
    }
}
