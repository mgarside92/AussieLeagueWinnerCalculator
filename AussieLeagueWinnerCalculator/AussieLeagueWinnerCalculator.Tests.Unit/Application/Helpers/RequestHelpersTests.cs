using AussieLeagueWinnerCalculator.Helpers;
using Shouldly;
using Xunit;

namespace AussieLeagueWinnerCalculator.UnitTests.Application.Helpers;

public class RequestHelpersTests
{

    [Theory]
    [InlineData(1, 1)]
    [InlineData(3, 6)]
    [InlineData(10, 3628800)]
    public void HeapsAlgorithm_GivenInt_ShouldReturnAllPermutations(int teamLength, int expectedTotalPermutations)
    {
        //Arrange & Act
        var result = RequestHelpers.HeapsAlgorithm(teamLength);
        
        //Assert
        result.ShouldBe(expectedTotalPermutations);
    }
}