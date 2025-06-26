using AussieLeagueWinnerCalculator.Application.Models;
using AussieLeagueWinnerCalculator.Application.Services;
using AussieLeagueWinnerCalculator.Models;
using AutoFixture.Xunit2;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shouldly;
using Xunit;

namespace AussieLeagueWinnerCalculator.UnitTests.Application.Services;

public class LeagueServiceTests
{
    private readonly ILeagueResultCalculator _leagueResultCalculator;

    private readonly LeagueService _sut;

    public LeagueServiceTests()
    {
        _leagueResultCalculator = Substitute.For<ILeagueResultCalculator>();
        
        _sut = new LeagueService(_leagueResultCalculator);
    }


    [Theory]
    [MemberData(nameof(ValidRequests))]
    public void GetWinnerAndRunnerUp_GivenValidResult_ShouldReturnCorrectResult(GetLeagueWinnerAndRunnerUpRequest request, List<TeamLeagueResult> calculatorResult, string[] expectedOutcome)
    {
        //Arrange
        _leagueResultCalculator.GetLeagueResult(request).Returns(calculatorResult);
        
        //Act
        var result = _sut.GetWinnerAndRunnerUp(request);
        
        //Assert
        result.ShouldBeEquivalentTo(expectedOutcome);
    }

    [Theory]
    [InlineAutoData]
    public void GetWinnerAndRunnerUp_GivenException_ShouldReturnEmptyArray(GetLeagueWinnerAndRunnerUpRequest request)
    {
        //Arrange
        _leagueResultCalculator.GetLeagueResult(request).Throws(new Exception());
        
        //Act
        var result = _sut.GetWinnerAndRunnerUp(request);
        
        //Assert
        result.ShouldBe(["", ""]);
    }
    
    public static IEnumerable<object[]> ValidRequests()
    {
        yield return
        [
            new GetLeagueWinnerAndRunnerUpRequest(["a:teamA", "b:teamB"], ["a:b", "b:a"], ["52:32", "22:56"]),
            new List<TeamLeagueResult> { new("a", "teamA", 8, 2), new("b", "teamB", 0, 0.5) },
            new[] { "a", "b" }
        ];
        yield return
        [
            new GetLeagueWinnerAndRunnerUpRequest(["a:teamA", "b:teamB", "c:teamC"], ["a:b", "b:a", "c:a", "c:b", "a:c", "b:c"], ["52:32", "22:56", "52:32", "22:56", "52:32", "22:56"]),
            new List<TeamLeagueResult> { new("a", "teamA", 12, (double)192/138), new("b", "teamB", 4, (double)132/186), new("c", "teamC", 8, (double)162/162) },
            new[] { "a", "c" }
        ];
    }
}