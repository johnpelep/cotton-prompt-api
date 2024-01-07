﻿using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Extensions;
using CottonPrompt.Infrastructure.Models.OutputSizes;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Services.OutputSizes
{
    public class OutputSizeService(CottonPromptContext dbContext) : IOutputSizeService
    {
        public async Task CreateAsync(string value, Guid userId)
        {
            try
            {
                var sortOrder = await dbContext.OrderOutputSizes
                    .OrderByDescending(db => db.SortOrder)
                    .Select(db => db.SortOrder + 1)
                    .FirstOrDefaultAsync();

                var designBracket = new OrderOutputSize
                {
                    Value = value,
                    CreatedBy = userId,
                    SortOrder = sortOrder,
                    Active = true,
                };

                await dbContext.OrderOutputSizes.AddAsync(designBracket);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await dbContext.OrderOutputSizes.Where(db => db.Id == id).ExecuteDeleteAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DisableAsync(int id, Guid userId)
        {
            try
            {
                await dbContext.OrderOutputSizes
                    .Where(db => db.Id == id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(db => db.Active, false)
                        .SetProperty(db => db.UpdatedBy, userId)
                        .SetProperty(db => db.UpdatedOn, DateTime.UtcNow));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EnableAsync(int id, Guid userId)
        {
            try
            {
                await dbContext.OrderOutputSizes
                    .Where(db => db.Id == id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(db => db.Active, true)
                        .SetProperty(db => db.UpdatedBy, userId)
                        .SetProperty(db => db.UpdatedOn, DateTime.UtcNow));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<OutputSize>> GetAsync(bool hasActiveFilter, bool active)
        {
            try
            {
                var designBrackets = await dbContext.OrderOutputSizes
                    .Where(db => !hasActiveFilter || hasActiveFilter && db.Active == active)
                    .OrderBy(db => db.SortOrder)
                    .ToListAsync();
                var result = designBrackets.AsModel();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetOutputSizeOrdersCountModel> GetOrdersCountAsync(int id)
        {
            try
            {
                var result = new GetOutputSizeOrdersCountModel
                {
                    Count = await dbContext.Orders.CountAsync(o => o.OutputSizeId == id)
                };
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SwapAsync(int id1, int id2, Guid userId)
        {
            try
            {
                var designBracket1 = await dbContext.OrderOutputSizes.FindAsync(id1);
                var designBracket2 = await dbContext.OrderOutputSizes.FindAsync(id2);

                if (designBracket1 is null || designBracket2 is null) return;

                (designBracket2.SortOrder, designBracket1.SortOrder) = (designBracket1.SortOrder, designBracket2.SortOrder);
                designBracket1.UpdatedBy = userId;
                designBracket1.UpdatedOn = DateTime.UtcNow;
                designBracket2.UpdatedBy = userId;
                designBracket2.UpdatedOn = DateTime.UtcNow;

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(int id, string value, Guid userId)
        {
            try
            {
                await dbContext.OrderOutputSizes
                    .Where(db => db.Id == id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(db => db.Value, value)
                        .SetProperty(db => db.UpdatedBy, userId)
                        .SetProperty(db => db.UpdatedOn, DateTime.UtcNow));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
