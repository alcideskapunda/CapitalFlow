using CapitalFlow.Api.Features.Common.Pagination;
using CapitalFlow.Api.Features.Users.Common;

namespace CapitalFlow.Api.Features.Users.GetUsers;

public record GetUsersQuery : PaginationQuery<UserViewModel> { }
