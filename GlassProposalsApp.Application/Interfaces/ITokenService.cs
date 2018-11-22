using GlassProposalsApp.Data.Enumerations;
using System;

namespace GlassProposalsApp.API.Interfaces
{
    public interface ITokenService
    {
        string Generate(string email, int securityLevel);
    }
}
