﻿using CottonPrompt.Infrastructure.Models.DesignBrackets;

namespace CottonPrompt.Infrastructure.Models.Orders
{
    public record Order(
        int Id, 
        string Number, 
        bool IsPriority,
        string Concept,
        string PrintColor,
        DesignBracket DesignBracket,
        IEnumerable<string> ImageReferences,
        DateTime CreatedOn
    );
}