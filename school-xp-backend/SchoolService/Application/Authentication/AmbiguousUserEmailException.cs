namespace SchoolService.Application.Authentication;

public sealed class AmbiguousUserEmailException : InvalidOperationException
{
    public AmbiguousUserEmailException()
        : base("Multiple user profiles have this email address; the account cannot be resolved automatically.")
    {
    }
}
