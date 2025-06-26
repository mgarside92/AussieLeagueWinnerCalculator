using AussieLeagueWinnerCalculator.Application.Models;

namespace AussieLeagueWinnerCalculator.Application.Services;

public class LeagueService(ILeagueResultCalculator leagueResultCalculator) : ILeagueService
{

    public string[] GetWinnerAndRunnerUp(GetLeagueWinnerAndRunnerUpRequest request)
    {
        try
        {
            var teamLeagueResults = leagueResultCalculator.GetLeagueResult(request);

            var winnerAndRunnerUp = teamLeagueResults.OrderByDescending(x => x.TotalPoints)
                .ThenByDescending(x => x.PointsScoredForAgainstRatio)
                .Select(x => x.TeamLetter)
                .Take(2)
                .ToArray();
            
            return winnerAndRunnerUp;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ["", ""];
        }
    }
}
