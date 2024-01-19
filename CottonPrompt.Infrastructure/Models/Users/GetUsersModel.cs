﻿namespace CottonPrompt.Infrastructure.Models.Users
{
    public record GetUsersModel(
        Guid Id, 
        string Name, 
        string Email, 
        string? Role
    );
}
