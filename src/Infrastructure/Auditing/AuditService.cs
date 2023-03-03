using CheckflixApp.Application.Auditing.Common;
using CheckflixApp.Application.Auditing.Interfaces;
using CheckflixApp.Infrastructure.Persistence;

namespace CheckflixApp.Infrastructure.Auditing;
public class AuditService : IAuditService
{
    // TODO: Auditing Service
    private readonly ApplicationDbContext _context;

    public AuditService(ApplicationDbContext context) => _context = context;

    public async Task<List<AuditDto>> GetUserTrailsAsync(string userId)
    {
        //var trails = await _context.AuditTrails
        //    .Where(a => a.UserId == userId)
        //    .OrderByDescending(a => a.DateTime)
        //    .Take(250)
        //    .ToListAsync();

        //return trails.Adapt<List<AuditDto>>();
        return new List<AuditDto>();
    }
}