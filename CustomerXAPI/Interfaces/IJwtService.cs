﻿namespace GitHubSearchAPI.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string username);
    }
}
