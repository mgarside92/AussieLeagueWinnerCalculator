using AussieLeagueWinnerCalculator.Application.Models;
using AussieLeagueWinnerCalculator.Helpers;
using AussieLeagueWinnerCalculator.Models;
using FluentValidation;

namespace AussieLeagueWinnerCalculator.Validators;

public class GetLeagueWinnerAndRunnerUpRequestValidator : AbstractValidator<GetLeagueWinnerAndRunnerUpRequest>
{
    public GetLeagueWinnerAndRunnerUpRequestValidator()
    {
        RuleFor(x => x)
            .Must(x => x.MatchFixtures.Length == x.MatchResults.Length)
            .WithMessage("Match fixtures doesn't match number of match results");
        
        RuleFor(x => x)
            .Must(x => x.MatchFixtures.Length == RequestHelpers.HeapsAlgorithm(x.Teams.Length))
            .WithMessage("Match fixtures don't align with the number of teams");        
        
        RuleFor(x => x.Teams)
            .NotEmpty()
            .WithMessage("Teams cannot be empty");
        RuleFor(x => x.MatchFixtures)
            .NotEmpty()
            .WithMessage("Match fixtures cannot be empty");
        RuleFor(x => x.MatchResults)
            .NotEmpty()
            .WithMessage("Match results cannot be empty");
        
        RuleForEach(x => x.Teams)
            .NotEmpty()
            .Must(x => x.Count(f => f == ':') == 1)
            .WithMessage("Team entry is malformed");
        RuleForEach(x => x.MatchFixtures)
            .NotEmpty()
            .Must(x => x.Count(f => f == ':') == 1)
            .WithMessage("Fixture entry is malformed");
        RuleForEach(x => x.MatchResults)
            .NotEmpty()
            .Must(x => x.Count(f => f == ':') == 1)
            .WithMessage("Result entry is malformed");
    }
}