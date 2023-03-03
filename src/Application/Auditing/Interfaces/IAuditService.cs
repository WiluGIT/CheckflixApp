using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckflixApp.Application.Auditing.Common;

namespace CheckflixApp.Application.Auditing.Interfaces;

public interface IAuditService
{
    Task<List<AuditDto>> GetUserTrailsAsync(string userId);
}