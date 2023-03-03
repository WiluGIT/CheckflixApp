using CheckflixApp.Application.Auditing.Common;
using CheckflixApp.Application.Auditing.Interfaces;
using CheckflixApp.Application.Common.Interfaces;
using MediatR;

namespace CheckflixApp.Application.Auditing.Queries;

public class GetMyAuditLogsQueryHandler : IRequestHandler<GetMyAuditLogsQuery, List<AuditDto>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IAuditService _auditService;

    public GetMyAuditLogsQueryHandler(ICurrentUserService currentUserService, IAuditService auditService) =>
        (_currentUserService, _auditService) = (currentUserService, auditService);

    public Task<List<AuditDto>> Handle(GetMyAuditLogsQuery request, CancellationToken cancellationToken) =>
        _auditService.GetUserTrailsAsync(_currentUserService.UserId);
}