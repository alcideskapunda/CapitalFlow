using CapitalFlow.Api.Features.Common.Pagination;
using CapitalFlow.Api.Features.Users.Common;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CapitalFlow.Api.Features.Users.GetUsers;

public class GetUsersEndpoint : Endpoint<GetUsersQuery, Results<Ok<PaginatedResult<UserViewModel>>, ProblemDetails>>
{
    public override void Configure()
    {
        Get("/users");
        Summary(s =>
        {
            s.Summary = "List users (paginated)";
            s.Description =
                "Returns users ordered by creation date (newest first). Results can be narrowed by creation date, a case-insensitive partial match on name, or an exact match on email.";
            s.AddPaginationQueryParams<GetUsersQuery, UserViewModel>();
        });
        Description(desc => desc
            .WithTags("Users"));
    }

    public override async Task<Results<Ok<PaginatedResult<UserViewModel>>, ProblemDetails>> ExecuteAsync(GetUsersQuery request, CancellationToken ct)
    {
        var result = await request.ExecuteAsync(ct);
        return TypedResults.Ok(result);
    }
}
