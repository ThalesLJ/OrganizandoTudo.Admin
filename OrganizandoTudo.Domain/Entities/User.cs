﻿using OrganizandoTudo.Domain.Entities.Base;

namespace OrganizandoTudo.Domain.Entities
    {
        public class User : BaseEntity
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string PasswordHash { get; set; }
        }
    }