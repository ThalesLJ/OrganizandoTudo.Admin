using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizandoTudo.Application.DTOs.Auth
{
    public record LoginRequestDto
    {
        public required string Username { get; init; }
        public required string Password { get; init; }
    }
}
