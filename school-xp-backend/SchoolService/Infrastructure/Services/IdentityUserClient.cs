using System.Net.Http.Headers;
using System.Net.Http.Json;
using SchoolService.Application.Abstractions;

namespace SchoolService.Infrastructure.Services;

public class IdentityUserClient(HttpClient httpClient) : IIdentityUserClient
{
    public async Task<Guid> CreateOrGetUserAsync(
        string email,
        string firstName,
        string lastName,
        string accessToken,
        CancellationToken ct)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "api/Auth/create-user");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Content = JsonContent.Create(new
        {
            userEmail = email,
            firstName,
            lastname = lastName,
        });

        using var response = await httpClient.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();

        var userId = await response.Content.ReadFromJsonAsync<Guid>(cancellationToken: ct);
        if (userId == Guid.Empty)
        {
            throw new InvalidOperationException("IdentityService returned an invalid user profile ID.");
        }

        return userId;
    }
}