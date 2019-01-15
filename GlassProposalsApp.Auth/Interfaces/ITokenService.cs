using GlassProposalsApp.Data.Enumerations;
using System;

namespace GlassProposalsApp.Auth.Interfaces
{
    public interface ITokenService
    {
        string Generate(string email, int securityLevel, Guid userId);
    }
}
