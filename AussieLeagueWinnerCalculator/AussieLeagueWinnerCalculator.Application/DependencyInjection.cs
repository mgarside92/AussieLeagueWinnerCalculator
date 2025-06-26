using AussieLeagueWinnerCalculator.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AussieLeagueWinnerCalculator.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ILeagueService, LeagueService>();
        services.AddScoped<ILeagueResultCalculator, LeagueResultCalculator>();
        
        return services;
    }
}