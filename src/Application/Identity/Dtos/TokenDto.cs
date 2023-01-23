﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckflixApp.Application.Identity.Dtos;
public record TokenDto(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);
