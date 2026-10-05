namespace SchoolService.Application.Abstractions;

public interface IIdentityDocumentProtector
{
    string Protect(string identifier);
}