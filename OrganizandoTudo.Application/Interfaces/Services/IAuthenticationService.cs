using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizandoTudo.Application.DTOs.Auth;

namespace OrganizandoTudo.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<AuthResult> LoginAsync(LoginRequestDto request);
        Task<AuthResult> RefreshTokenAsync(string refreshToken);
        Task RevokeTokenAsync(string refreshToken);
    }
}
