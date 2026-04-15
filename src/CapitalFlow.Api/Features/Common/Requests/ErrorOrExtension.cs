using ErrorOr;
using FluentValidation.Results;

namespace CapitalFlow.Api.Features.Common.Requests;

public static class ErrorOrExtension
{
    public static ProblemDetails ToProblemDetails<T>(this ErrorOr<T> result)
    {
        List<ValidationFailure> validationFailures = [];
        foreach (var error in result.Errors)
        {
            validationFailures.Add(new ValidationFailure(error.Code, error.Description));
        }

        var problem = new ProblemDetails(validationFailures)
        {
            Status = result.FirstError.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status400BadRequest
            }
        };

        return problem;
    }
}
