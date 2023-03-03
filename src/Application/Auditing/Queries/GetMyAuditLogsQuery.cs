using CheckflixApp.Application.Auditing.Common;
using MediatR;

namespace CheckflixApp.Application.Auditing.Queries;

public class GetMyAuditLogsQuery : IRequest<List<AuditDto>>
{
}