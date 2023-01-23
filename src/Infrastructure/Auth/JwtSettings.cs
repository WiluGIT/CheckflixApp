﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckflixApp.Infrastructure.Auth;
public class JwtSettings
{
    public string? Key { get; set; }

    public int TokenExpirationInMinutes { get; set; }

    public int RefreshTokenExpirationInDays { get; set; }
}
