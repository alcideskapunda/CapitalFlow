using CapitalFlow.Api.Features.Common.Requests;
using CapitalFlow.Api.Features.Users.Common;
using Microsoft.AspNetCore.Mvc;

namespace CapitalFlow.Api.Features.Users.GetUserById;

public record GetUserByIdQuery : IQuery<UserViewModel?>
{
    [FromRoute]
    public Guid Id { get; set; }
}
