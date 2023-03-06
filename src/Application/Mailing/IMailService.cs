namespace CheckflixApp.Application.Mailing;
public interface IMailService
{
    Task SendAsync(MailRequest request, CancellationToken ct);
}