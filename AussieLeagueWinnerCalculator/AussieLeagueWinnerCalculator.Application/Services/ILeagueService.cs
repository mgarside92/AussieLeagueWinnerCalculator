using AussieLeagueWinnerCalculator.Application.Models;

namespace AussieLeagueWinnerCalculator.Application.Services;

public interface ILeagueService
{
    string[] GetWinnerAndRunnerUp(GetLeagueWinnerAndRunnerUpRequest request);
}