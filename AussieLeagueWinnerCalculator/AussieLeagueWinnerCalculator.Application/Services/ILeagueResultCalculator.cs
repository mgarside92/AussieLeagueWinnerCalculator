using AussieLeagueWinnerCalculator.Application.Models;

namespace AussieLeagueWinnerCalculator.Application.Services;

public interface ILeagueResultCalculator
{
    List<TeamLeagueResult> GetLeagueResult(GetLeagueWinnerAndRunnerUpRequest request);
}