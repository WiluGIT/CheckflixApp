using CheckflixApp.Application.Common.Interfaces;

namespace CheckflixApp.Infrastructure.Services;
public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
