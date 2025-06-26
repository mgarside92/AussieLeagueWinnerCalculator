using AussieLeagueWinnerCalculator.Application.Models;

namespace AussieLeagueWinnerCalculator.Application.Services;

public class LeagueResultCalculator : ILeagueResultCalculator
{
    public List<TeamLeagueResult> GetLeagueResult(GetLeagueWinnerAndRunnerUpRequest request)
    {
        var teamLeagueResults = new List<TeamLeagueResult>();

        foreach (var team in request.Teams)
        {
            var teamLetterAndName = team.Split(':');
                
            var teamLetter = teamLetterAndName[0];
            var teamName = teamLetterAndName[1];

            var teamPoints = 0;
            var teamScoredFor = 0;
            var teamScoredAgainst = 0;
                
            for (var i = 0; i < request.MatchFixtures.Length; i++)
            {
                var fixture = request.MatchFixtures[i];
                if (!fixture.Contains(teamLetter)) continue;
                    
                var matchUp = fixture.Split(':');
                var teamIndex = matchUp[0].Contains(teamLetter) ? 0 : 1;
                var opponentIndex = (teamIndex + 1) % 2;
                        
                var matchResult = request.MatchResults[i].Split(':').Select(int.Parse).ToArray();

                if (matchResult[teamIndex] > matchResult[opponentIndex])
                    teamPoints += 4;
                        
                if (matchResult[teamIndex] == matchResult[opponentIndex])
                    teamPoints += 2;
                        
                teamScoredFor += matchResult[teamIndex];
                teamScoredAgainst += matchResult[opponentIndex];
            }

            var teamLeagueResult = new TeamLeagueResult(teamLetter, teamName, teamPoints,
                 (double)teamScoredFor / teamScoredAgainst);
            
            teamLeagueResults.Add(teamLeagueResult);
        }

        return teamLeagueResults;
    }
}