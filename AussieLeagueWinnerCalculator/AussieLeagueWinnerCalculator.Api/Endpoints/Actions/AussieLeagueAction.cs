using AussieLeagueWinnerCalculator.Application.Models;
using AussieLeagueWinnerCalculator.Application.Services;
using AussieLeagueWinnerCalculator.Models;
using AussieLeagueWinnerCalculator.Validators;
using Microsoft.AspNetCore.Mvc;

namespace AussieLeagueWinnerCalculator.Endpoints.Actions;

public class AussieLeagueAction
{
    public static IResult GetLeagueWinnerAndRunnerUp(
        GetLeagueWinnerAndRunnerUpRequest request,
        HttpContext httpContext,
        [FromServices] GetLeagueWinnerAndRunnerUpRequestValidator validator,
        [FromServices] ILeagueService winnerCalculator)
    {
        var isValid = validator.Validate(request);
        if (!isValid.IsValid)
            return Results.BadRequest(isValid.Errors);
        
        var result = winnerCalculator.GetWinnerAndRunnerUp(request);
        
        return Results.Ok(result);
    }
}