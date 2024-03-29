﻿using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.Identity.Tokens.Queries.GetRefreshToken;
public record GetRefreshTokenQuery(string Token) : IRequest<Result<AccessTokenDto>>;