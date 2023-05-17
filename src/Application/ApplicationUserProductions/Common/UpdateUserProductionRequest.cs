using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckflixApp.Domain.Common.Primitives.Result;
using MediatR;

namespace CheckflixApp.Application.ApplicationUserProductions.Common;

public record UpdateUserProductionRequest(bool? Favourites, bool? ToWatch, bool? Watched);