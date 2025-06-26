using AussieLeagueWinnerCalculator.Endpoints.Actions;
using AussieLeagueWinnerCalculator.Models;
using Microsoft.AspNetCore.Mvc;

namespace AussieLeagueWinnerCalculator.Endpoints;

public static class Endpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapPost("/api/GetLeagueWinnerAndRunnerUp", AussieLeagueAction.GetLeagueWinnerAndRunnerUp)
            .Produces(200)
            .ProducesStandardProblems();

        return app;
    }

    private static void ProducesStandardProblems(this RouteHandlerBuilder builder)
    {
        builder
            .Produces<ProblemDetails>(401, ProblemDetailsMediaType.ProblemJson)
            .Produces<ProblemDetails>(403, ProblemDetailsMediaType.ProblemJson)
            .Produces<ProblemDetails>(500, ProblemDetailsMediaType.ProblemJson);
    }
}