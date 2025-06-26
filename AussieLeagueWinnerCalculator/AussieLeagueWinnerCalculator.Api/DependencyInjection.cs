using AussieLeagueWinnerCalculator.Validators;
using FluentValidation;

namespace AussieLeagueWinnerCalculator;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<GetLeagueWinnerAndRunnerUpRequestValidator>();
        
        return services;
    }
}