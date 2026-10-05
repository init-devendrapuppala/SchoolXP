namespace SchoolService.API.Contracts;

public sealed record GuardianTypeDto(
    int GuardianTypeId,
    string Code,
    string Description);