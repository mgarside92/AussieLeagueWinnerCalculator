using AussieLeagueWinnerCalculator.Application.Models;
using AussieLeagueWinnerCalculator.Application.Services;
using Shouldly;
using Xunit;

namespace AussieLeagueWinnerCalculator.UnitTests.Application.Services;

public class LeagueResultCalculatorTests
{
    private readonly LeagueResultCalculator _sut = new();

    [Theory]
    [MemberData(nameof(ValidRequests))]
    public void GetLeagueResult_GivenValidRequest_ShouldReturnListOfLeagueResults(
        GetLeagueWinnerAndRunnerUpRequest request, List<TeamLeagueResult> expectedTeamResults)
    {
        //Arrange & Act
        var results = _sut.GetLeagueResult(request);
        
        //Assert
        results.ShouldBeEquivalentTo(expectedTeamResults);
    }
    
    public static IEnumerable<object[]> ValidRequests()
    {
        yield return
        [
            new GetLeagueWinnerAndRunnerUpRequest(["a:teamA", "b:teamB"], ["a:b", "b:a"], ["52:32", "22:56"] ),
            new List<TeamLeagueResult> { new("a", "teamA", 8, 2), new("b", "teamB", 0, 0.5) }
        ];
        yield return
        [
            new GetLeagueWinnerAndRunnerUpRequest(["a:teamA", "b:teamB", "c:teamC"], ["a:b", "b:a", "c:a", "c:b", "a:c", "b:c"], ["52:32", "22:56", "52:32", "22:56", "52:32", "22:56"]),
            new List<TeamLeagueResult> { new("a", "teamA", 12, (double)192/138), new("b", "teamB", 4, (double)132/186), new("c", "teamC", 8, (double)162/162) }
        ];
    }
}