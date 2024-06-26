﻿using CottonPrompt.Infrastructure.Models.Comments;

namespace CottonPrompt.Infrastructure.Models.Designs
{
    public record DesignModel(int Id, string Name, string Url, DateTime CreatedOn, IEnumerable<CommentModel> Comments);
}
