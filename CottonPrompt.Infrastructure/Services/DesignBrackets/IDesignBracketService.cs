﻿using CottonPrompt.Infrastructure.Models.DesignBrackets;

namespace CottonPrompt.Infrastructure.Services.DesignBrackets
{
    public interface IDesignBracketService
    {
        Task<IEnumerable<DesignBracket>> GetAsync(bool hasActiveFilter, bool active);

        Task SwapAsync(int id1, int id2, Guid userId);

        Task UpdateAsync(int id, string name, decimal value, Guid userId);

        Task<GetDesignBracketOrdersCountModel> GetOrdersCountAsync(int id);

        Task DeleteAsync(int id);

        Task DisableAsync(int id, Guid userId);

        Task EnableAsync(int id, Guid userId);

        Task CreateAsync(string name, decimal value, Guid userId);
    }
}
