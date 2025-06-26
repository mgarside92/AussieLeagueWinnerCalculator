namespace AussieLeagueWinnerCalculator.Application.Models;

public record GetLeagueWinnerAndRunnerUpRequest(string[] Teams, string[] MatchFixtures, string[] MatchResults);
