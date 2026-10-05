using Microsoft.AspNetCore.DataProtection;
using SchoolService.Application.Abstractions;

namespace SchoolService.Infrastructure.Services;

public class DataProtectionIdentityDocumentProtector : IIdentityDocumentProtector
{
    private readonly IDataProtector protector;

    public DataProtectionIdentityDocumentProtector(IDataProtectionProvider provider)
    {
        protector = provider.CreateProtector("SchoolXP.PersonIdentityDocument.Aadhaar.v1");
    }

    public string Protect(string identifier) => protector.Protect(identifier);
}