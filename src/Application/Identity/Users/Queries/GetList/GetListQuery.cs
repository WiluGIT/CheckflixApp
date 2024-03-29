﻿using CheckflixApp.Application.Common.Security;
using CheckflixApp.Application.Identity.Common;
using MediatR;
using CheckflixApp.Domain.Common.Primitives.Result;

namespace CheckflixApp.Application.Identity.Users.Queries.GetList;

//TODO: FOR ADMIN [Authorize(Roles = "Administrator")]
[Authorize]
public record GetListQuery() : IRequest<Result<List<UserDetailsDto>>>;