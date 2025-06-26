using AussieLeagueWinnerCalculator.Application.Models;
using AussieLeagueWinnerCalculator.Validators;
using Shouldly;
using Xunit;

namespace AussieLeagueWinnerCalculator.UnitTests.Api.Validators;

public class GetLeagueWinnerAndRunnerUpRequestValidatorTests
{
    private readonly GetLeagueWinnerAndRunnerUpRequestValidator _sut = new();

    [Theory]
    [MemberData(nameof(ValidRequests))]
    [MemberData(nameof(InvalidRequests))]
    public void Validate_RequestWithParams_ShouldGetExpectedResult(string[] teams, string[] fixtures, string[] results, bool expectedResult)
    {
        //Arrange
        var request = new GetLeagueWinnerAndRunnerUpRequest(teams, fixtures, results);
        
        //Act
        var result = _sut.Validate(request);
        
        //Assert
        result.IsValid.ShouldBe(expectedResult);
    }
    
    public static IEnumerable<object[]> ValidRequests()
    {
        yield return [new[] {"a:teamA", "b:teamB"}, new []{"a:b", "b:a"}, new []{"52:32","22:56"}, true];
        yield return [new[] {"a:teamA", "b:teamB", "c:teamC"}, new []{"a:b", "b:a", "c:a", "c:b", "a:c", "b:c"}, new []{"52:32", "22:56", "52:32", "22:56", "52:32", "22:56"}, true];
    }

    public static IEnumerable<object[]> InvalidRequests()
    {
        yield return [new[] {"a:teamA"}, new []{"a:b", "b:a"}, new []{"52:32","22:56"}, false];
        yield return [new[] {"a:teamA"}, new []{"a:b", "b:a"}, new []{"52:32"}, false];
        yield return [new[] {"a:teamA", "b:teamB"}, new []{"a:b"}, new []{"52:32"}, false];
        yield return [new[] {"a:teamA", ""}, new []{"a:b", "b:a"}, new []{"52:32","22:56"}, false];
        yield return [new[] {"a:teamA", "b::teamB"}, new []{"a:b", "b:a"}, new []{"52:32","22:56"}, false];
        yield return [new[] {"a:teamA", "b:teamB"}, new []{"a:b:c", "b:a"}, new []{"52:32","22:56"}, false];
        yield return [new[] {"a:teamA", "b:teamB"}, new []{"a:b", "b:a"}, new []{"52:32:73:12","22:56"}, false];
    }
}