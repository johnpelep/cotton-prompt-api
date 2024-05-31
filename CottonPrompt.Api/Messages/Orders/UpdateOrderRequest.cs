﻿using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Orders
{
    public class UpdateOrderRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string OrderNumber { get; set; }

        [Required]
        public bool Priority { get; set; }

        [Required]
        public string Concept { get; set; }

        [Required]
        public int PrintColorId { get; set; }

        [Required]
        public int DesignBracketId { get; set; }

        [Required]
        public int OutputSizeId { get; set; }

        [Required]
        public string CustomerEmail { get; set; }

        [Required]
        public int UserGroupId { get; set; }

        public IEnumerable<ImageReferenceRequest>? ImageReferences { get; set; }

        [Required]
        public Guid UpdatedBy { get; set; }
    }
}
