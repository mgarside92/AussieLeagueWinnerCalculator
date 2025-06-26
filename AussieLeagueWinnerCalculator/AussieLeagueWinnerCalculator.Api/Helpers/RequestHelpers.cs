namespace AussieLeagueWinnerCalculator.Helpers;

public static class RequestHelpers
{
    public static int HeapsAlgorithm(int teamLength)
    {
        var totalPermutations = teamLength;
        for (var i = 1; teamLength - i > 0; i++)
        {
            totalPermutations *= (teamLength - i);
        }
        
        return totalPermutations;
    }
}